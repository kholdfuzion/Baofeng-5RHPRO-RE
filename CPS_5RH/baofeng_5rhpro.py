# Copyright 2025 Larry Ficken
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.

"""Baofeng 5RHPRO radio management module"""

import time
import logging
import re
import struct
import random
from chirp import util, chirp_common, bitwise, errors, directory, memmap
from chirp.settings import RadioSetting, RadioSettingGroup, \
                RadioSettingValueBoolean, RadioSettingValueList, \
                RadioSettingValueInteger, RadioSettingValueString, \
                RadioSettingValueFloat, RadioSettings

LOG = logging.getLogger(__name__)

# Constants from the C# code
DP_DATA_LEN = 49152
ZONE_MAX_NUM = 10
ZONE_MAX_CHN_NUM = 64
MAX_CHN_NUM = ZONE_MAX_NUM*ZONE_MAX_CHN_NUM
MAX_SCAN_LIST_NUM = 16
MAX_EMERG_SYS_NUM = 10
MAX_GPSBOOK_NUM = 80

# State enum
(STATE_HANDSHAKE1, STATE_HANDSHAKE2, STATE_HANDSHAKE3, 
 STATE_HANDSHAKE4, STATE_HANDSHAKE5, STATE_HANDSHAKE6, 
 STATE_READ1, STATE_READ2, STATE_READ3,
 STATE_WRITE1, STATE_WRITE2, STATE_WRITE3) = range(12)

def _bf5rhpro_prep(radio):
    """Initialize communication with the radio following the protocol states"""
    # Initialize timeout settings
    radio.pipe.timeout = 1
    
    # T_Info packet for handshake
    T_Info = bytes([0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255])
    radio.pipe.write(T_Info)
    
    # Wait for 'A' response
    retries = 10
    ack = None
    while retries > 0:
        try:
            ack = radio.pipe.read(1)
            if ack == b'A':
                break
        except Exception as e:
            LOG.warning(f"Read error: {e}")
        
        retries -= 1
        time.sleep(0.5)
        
        # On last retry, attempt baudrate change
        if retries == 0:
            radio.pipe.baudrate = 115200
            radio.pipe.write(T_Info)
            try:
                ack = radio.pipe.read(1)
                if ack == b'A':
                    break
            except Exception as e:
                LOG.warning(f"Read error at 115200 baud: {e}")
            
    if ack != b'A':
        raise errors.RadioError("Radio did not respond to initial command")
    
    # Set encryption seed value
    #seed = random.randint(1, 254)  # CPS uses random.randint(1, 254) for production
    seed = 0x00
    radio._seed = seed
    
    # Send PROGRAM command with seed
    Header_SYNC = bytearray(b"PROGRAM\0")
    Header_SYNC[7] = seed
    radio.pipe.write(Header_SYNC)
    
    # Wait for encrypted response
    ack = radio.pipe.read(1)
    if len(ack) == 0:
        raise errors.RadioError("Radio did not acknowledge PROGRAM command")
    
    decrypted = ack[0] ^ seed
    if decrypted != ord('A'):
        raise errors.RadioError("Radio did not acknowledge PROGRAM command")
    
    # Send password
    password = bytearray([255, 255, 255, 255, 255, 255, 255, 255])
    xor_password = bytearray(8)
    for i in range(8):
        xor_password[i] = password[i] ^ seed
    
    radio.pipe.write(xor_password)
    
    # Wait for password response
    ack = radio.pipe.read(1)
    if len(ack) == 0:
        raise errors.RadioError("Wrong password")
    
    decrypted = ack[0] ^ seed
    if decrypted != ord('A'):
        raise errors.RadioError("Wrong password")
    
    # Send INFORMATION command
    info_cmd = bytearray(11)
    for i in range(len(b"INFORMATION")):
        info_cmd[i] = b"INFORMATION"[i] ^ seed
    
    radio.pipe.write(info_cmd)
    
    # Read radio information
    info = radio.pipe.read(16)
    if len(info) != 16:
        raise errors.RadioError("Failed to read radio information")
    
    # Process radio information
    radio.is_k6 = False
    model_bytes = bytearray(16)
    for i in range(16):
        model_bytes[i] = info[i] ^ seed
        if model_bytes[i] == 0xFF:
            break
    
    # Decode model string
    model_str = bytes(model_bytes[:i]).decode('ascii', errors='ignore')
    if model_str == "BF-K6":
        radio.is_k6 = True
    
    # Get frequency band info
    radio.freq_band = info[8] ^ seed
    
    return seed

def bf5rhpro_download(radio):
    """Download configuration data from the radio"""
    try:
        # Run initialization/handshake process
        seed = _bf5rhpro_prep(radio)
        
        # Send read command
        cmd_byte = ord('R') ^ seed
        radio.pipe.write(bytes([cmd_byte]))
        
        # Check acknowledgment
        ack = radio.pipe.read(1)
        if len(ack) == 0:
            raise errors.RadioError("Radio did not acknowledge read command")
        
        decrypted = ack[0] ^ seed
        if decrypted != ord('A'):
            raise errors.RadioError("Radio did not acknowledge read command")
        
        # Initialize data buffer
        data = bytearray(DP_DATA_LEN)
        num = 0
        read_count = 0
        original_timeout = radio.pipe.timeout
        
        # Data reading loop
        while read_count < DP_DATA_LEN:
            radio.pipe.timeout = 4
            
            times_for_retry = 3
            retry_count = 0
            success = False
            
            while retry_count <= times_for_retry and not success:
                try:
                    # Prepare read command with address
                    cmd = bytearray([
                        ord('R') ^ seed,
                        (num >> 8) & 0xFF, 
                        num & 0xFF, 
                        0
                    ])
                    radio.pipe.write(cmd)
                    
                    # Read block (4100 bytes: 4-byte header + 4096 data bytes)
                    block = radio.pipe.read(4100)
                    
                    if len(block) == 4100:
                        success = True
                    else:
                        LOG.warning(f"Short read: {len(block)} bytes (expected 4100)")
                        retry_count += 1
                        time.sleep(0.5)
                
                except Exception as e:
                    LOG.warning(f"Error during read: {e}")
                    retry_count += 1
                    time.sleep(0.5)
            
            # Restore original timeout
            radio.pipe.timeout = original_timeout
            
            if not success:
                raise errors.RadioError("Failed to read data from radio after retries")
            
            # Copy data (skip the 4-byte header)
            data_len = min(4096, DP_DATA_LEN - read_count)
            data[read_count:read_count + data_len] = block[4:4 + data_len]
            
            read_count += data_len
            num += data_len
            
            # Update status if callback is available
            if radio.status_fn:
                class Status:
                    def __init__(self, current, maximum=100, message=""):
                        self.cur = current
                        self.max = maximum
                        self.msg = message
                
                percent = int(read_count / DP_DATA_LEN * 100)
                msg = f"Downloading from {radio.MODEL}: {percent}%"
                status = Status(percent, 100, msg)
                radio.status_fn(status)
        
        # Send END command
        end_cmd = bytearray(4)
        for i in range(4):
            end_cmd[i] = b"END\0"[i] ^ seed
            
        radio.pipe.write(end_cmd)
        
        # Wait for final acknowledgment
        ack = radio.pipe.read(1)
        if len(ack) == 0:
            raise errors.RadioError("Radio did not acknowledge end of download")
        
        decrypted = ack[0] ^ seed
        if decrypted != ord('A'):
            raise errors.RadioError("Radio did not acknowledge end of download")
        
        # XOR decrypt the data
        for i in range(len(data)):
            data[i] ^= seed
        
        return memmap.MemoryMap(bytes(data))
    
    except Exception as e:
        raise errors.RadioError(f"Failed to communicate with radio: {e}")

def bf5rhpro_upload(radio):
    """Upload configuration data to the radio"""
    try:
        # Run initialization/handshake process
        seed = _bf5rhpro_prep(radio)
        
        # Send write command
        cmd_byte = ord('W') ^ seed
        radio.pipe.write(bytes([cmd_byte]))
        
        # Check acknowledgment
        ack = radio.pipe.read(1)
        if len(ack) == 0:
            raise errors.RadioError("Radio did not acknowledge write command")
        
        decrypted = ack[0] ^ seed
        if decrypted != ord('A'):
            raise errors.RadioError("Radio did not acknowledge write command")
        
        # Get data from memory map
        data = radio.get_mmap().get_byte_compatible()
        addr = 0
        
        # Data writing loop
        while addr < DP_DATA_LEN:
            # Calculate size to write (4096 bytes per block)
            size = min(4096, DP_DATA_LEN - addr)
            
            # Prepare write command with header and data
            cmd = bytearray([
                ord('W') ^ seed, 
                (addr >> 8) & 0xFF, 
                addr & 0xFF, 
                0  # Padding byte
            ])
            
            # Add XOR-encrypted data block
            data_block = bytearray(size)
            for i in range(size):
                # Handle different data types appropriately
                if isinstance(data[addr + i], int):
                    data_block[i] = data[addr + i] ^ seed
                else:
                    data_block[i] = ord(data[addr + i]) ^ seed
            
            # Combine header and data
            cmd.extend(data_block)
            
            # Send command with data
            radio.pipe.write(cmd)
            
            # Wait for acknowledgment
            ack = radio.pipe.read(1)
            if len(ack) == 0:
                raise errors.RadioError("Radio did not acknowledge block write")
            
            decrypted = ack[0] ^ seed
            if decrypted != ord('A'):
                raise errors.RadioError("Radio did not acknowledge block write")
            
            addr += size
            
            # Update status if callback is available
            if radio.status_fn:
                class Status:
                    def __init__(self, current, maximum=100, message=""):
                        self.cur = current
                        self.max = maximum
                        self.msg = message

                percent = int(addr / DP_DATA_LEN * 100)
                msg = f"Uploading to {radio.MODEL}: {percent}%"
                status = Status(percent, 100, msg)
                radio.status_fn(status)
        
        # Send END command
        end_cmd = bytearray(4)
        for i in range(4):
            end_cmd[i] = b"END\0"[i] ^ seed
        radio.pipe.write(end_cmd)
        
        # Wait for final acknowledgment
        ack = radio.pipe.read(1)
        if len(ack) == 0:
            raise errors.RadioError("Radio did not acknowledge end of upload")
        
        decrypted = ack[0] ^ seed
        if decrypted != ord('A'):
            raise errors.RadioError("Radio did not acknowledge end of upload")
            
    except Exception as e:
        raise errors.RadioError(f"Failed to communicate with radio: {e}")   

# Memory structure for Baofeng 5RHPRO
MEM_FORMAT = """
struct {
  char dev_name[16];
  u32 rx_freq_ranges[8];
  u32 tx_freq_ranges[8];
  char soft_ver[8];
  char hard_ver[8];
  char prod_date[16];
  u8 freq_band;
  u8 limits;
  u8 scramble;
  u8 features1;
  u8 features2;
  u8 reserved1[3];
  u8 prokey[8];
} device_info;

struct {
  u32 rx_freq;               // RX Frequency (e.g., 0x44152500 -> 446.125 MHz)
  u32 tx_freq;               // TX Frequency (e.g., 0x44652500 -> 446.165 MHz)
  u8 rx_ctcss_dcs_h;         // RX CTCSS/DCS High Byte
  u8 rx_ctcss_dcs_l;         // RX CTCSS/DCS Low Byte
  u8 tx_ctcss_dcs_h;         // TX CTCSS/DCS High Byte
  u8 tx_ctcss_dcs_l;         // TX CTCSS/DCS Low Byte
  u32 div_freq;              // Divider Frequency
  u8 power:2,                // Power Level (0=Low, 2=High)
     wideth:2,               // Bandwidth (0=Narrow, 1=Wide)
     offsetdir:2,            // Offset Direction (0=None, 1=Plus, 2=Minus)
     freqinvert:1,           // Frequency Invert
     talkaround:1;           // Talkaround
  u8 fivetoneptt:2,          // Five-Tone PTT
     dtmfptt:2,              // DTMF PTT
     sqtype:4;               // Squelch Type
  u8 signaltype:3,           // Signal Type
     jumpfreq:2,             // Jump Frequency
     reserved1:3;            // Reserved
  u8 busylock:2,             // Busy Lock
     txdis:1,                // TX Disable
     reserved2:5;            // Reserved
  u8 scram:1,                // Scramble
     compand:1,              // Companding
     cepin_dcs:1,            // CTCSS/DCS Encryption
     cepin_24bit:1,          // 24-bit Encryption
     reserved3:4;            // Reserved
  u8 reserved4[3];           // Reserved
  u8 freq_step;              // Frequency Step
  u8 dtmf_idx;               // DTMF Index
  u8 twotone_idx;            // Two-Tone Index
  u8 fivetone_idx;           // Five-Tone Index
  u8 mdc_idx;                // MDC Index
  u8 scanlist;               // Scan List Index
  u8 emerglist;              // Emergency List Index
  u8 reserved5;              // Reserved
  char chn_name[16];         // Channel Name
} memory[640];

#seekto 0x7900;
struct {
    u32 rx_freq;             // Receive frequency
    u32 tx_freq;             // Transmit frequency
    u8 rx_ctcss_dcs_h;       // RX CTCSS/DCS High Byte
    u8 rx_ctcss_dcs_l;       // RX CTCSS/DCS Low Byte
    u8 tx_ctcss_dcs_h;       // TX CTCSS/DCS High Byte
    u8 tx_ctcss_dcs_l;       // TX CTCSS/DCS Low Byte
    u32 div_freq;            // Divider frequency
    u8 power : 2,            // Power level
       wideth : 1,           // Bandwidth (wide/narrow)
       offsetdir : 2,        // Offset direction
       freqinvert : 1,       // Frequency inversion
       talkaround : 1,       // Talkaround
       Reserved1 : 1;        // Reserved
    u8 fivetoneptt : 2,      // Five-tone PTT
       dtmfptt : 2,          // DTMF PTT
       sqtype : 4;           // Squelch type
    u8 signaltype : 3,       // Signal type
       jumpfreq : 2,         // Jump frequency
       Reserved2 : 3;        // Reserved
    u8 busylock : 2,         // Busy lock
       txdis : 1,            // Transmit disable
       Reserved3 : 5;        // Reserved
    u8 scram : 1,            // Scrambler
       compand : 1,          // Companding
       cepin_dcs : 1,        // CTCSS/DCS inversion
       cepin_24bit : 1,      // 24-bit encryption
       Reserved4 : 4;        // Reserved
    u8 Reserved5[3];         // Reserved
    u8 freq_step;            // Frequency step
    u8 dtmf_idx;             // DTMF index
    u8 twotone_idx;          // Two-tone index
    u8 fivetone_idx;         // Five-tone index
    u8 mdc_idx;              // MDC index
    u8 scanlist;             // Scan list
    u8 emerglist;            // Emergency list
    u8 Reserved6;            // Reserved
    char chn_name[16];       // VFO name
} vfo[2];

#seekto 0x7980;
struct {
  u8 ch_a_mode;              // Channel A mode (0=VFO, 1=MR)
  u8 ch_b_mode;              // Channel B mode (0=VFO, 1=MR)
  u16 ch_a_num;              // Channel A number
  u16 ch_b_num;              // Channel B number
  u8 ch_a_zone;              // Channel A zone
  u8 ch_b_zone;              // Channel B zone
  u8 blight_time;            // Backlight time
  u8 blight_lv;              // Backlight level
  u8 dispa_mode:4,           // Display mode for channel A
     dispb_mode:4;           // Display mode for channel B
  u8 dual_mode;              // Dual watch mode
  u8 main_band;              // Main band (0=A, 1=B)
  u8 sqlv;                   // Squelch level
  u8 vox_lv;                 // VOX level
  u8 vox_det_time;           // VOX detection time
  u8 po_save;                // Power save mode
  u8 po_save_dly;            // Power save delay
  u8 lone_work_tim;          // Lone worker timer
  u8 lone_work_rsp;          // Lone worker response time
  u8 apo;                    // Auto power off time
  u8 tot;                    // Timeout timer
  u8 pre_tot;                // Pre-timeout alert
  u8 reserved1;              // Reserved
  u8 gps_zone;               // GPS time zone
  u8 reserved2;              // Reserved
  u8 hz_to_1750;             // 1750Hz tone frequency
  u8 reserved3[3];           // Reserved
  u8 noaa_ch;                // NOAA channel
  u8 gps_id;                 // GPS ID
  u8 voxsw : 1,              // VOX switch
     aprssw : 1,             // APRS switch
     lonework : 1,           // Lone worker switch
     daodi : 1,              // Falling alarm switch
     voice : 2,              // Voice prompt (0=Off, 1=Chinese, 2=English)
     busylock : 2;           // Busy lock
  u8 keylock : 1,            // Keypad lock
     autokey : 1,            // Auto keypad lock
     reserved6 : 6;          // Reserved
  u8 tone : 1,               // Tone
     endtone : 2,            // End tone
     reserved7 : 5;          // Reserved
  u8 GpsSW : 1,              // GPS switch
     GpsMode : 2,            // GPS mode
     GpsShare : 1,           // GPS share
     GpsReq : 1,             // GPS request
     reserved8 : 3;          // Reserved
  u8 BlueT : 1,              // Bluetooth
     BTpair : 2,             // Bluetooth pairing
     BluetAPP : 1,           // Bluetooth app
     reserved9 : 4;          // Reserved
  u8 Reord : 1,              // Record
     RecordMode : 2,         // Record mode
     Engineering : 1,        // Engineering mode
     TianQi : 1,             // Weather
     LangSel : 1,            // Language selection
     PownFace : 2;           // Power-on face
  u8 TailFreq : 3,           // Tail frequency
     NOAA : 1,               // NOAA
     DispDir : 1,            // Display direction
     FmInter : 1,            // FM interference
     NoiseCancel : 1,        // Noise cancellation
     EnhanceFunc : 1;        // Enhanced function
  u8 reserved10;             // Reserved
  u8 bt_hold;                // Bluetooth hold time
  u8 bt_rxdly;               // Bluetooth RX delay
  u8 bt_mic;                 // Bluetooth mic gain
  u8 bt_spk;                 // Bluetooth speaker volume
  char bt_password[4];       // Bluetooth password
  u8 skey1;                  // Short key 1 function
  u8 skey2;                  // Short key 2 function
  u8 lkey1;                  // Long key 1 function
  u8 lkey2;                  // Long key 2 function
  u8 reserved4[12];          // Reserved
  char pow_password[8];      // Power-on password
  char wr_password[8];       // Write/programming password
  char radio_name[16];       // Radio name
  char bluet_name[16];       // Bluetooth name
  char pair_name[16];        // Pairing name
} settings;

#seekto 0x7A20;
struct {
    ul64 flags;              // 64-bit value for channel validity (0=valid, 1=invalid)
} chnvalid[10];              // 10 zones × 64 channels = 640 channels total

#seekto 0x7A80;
struct {
  u8 zone_total;             // Total number of zones
  u8 reserved[15];           // Reserved
  struct {
    u8 chn_num;              // Number of channels in zone
    u8 reserved;             // Reserved
    u16 chn_id[64];          // Channel IDs for this zone
    u8 reserved2[6];         // Reserved
    char zone_name[16];      // Zone name
  } zone_info[10];           // 10 zones
} zones;

#seekto 0x8100;
struct {
  u32 upfreq;                // Upper frequency for scan range
  u32 downfreq;              // Lower frequency for scan range
} scanranges[10];            // 10 scan ranges

#seekto 0x8180;
struct {
    u8 scanmode;             // Scan mode
    u8 backscantime;         // Background scan time
    u8 rxresumetime;         // RX resume time
    u8 txresumetime;         // TX resume time
    u8 returnchanneltype;    // Return channel type
    u8 priorityscan;         // Priority scan enable
    u16 prioritychannel;     // Priority channel
    u8 scanrange;            // Scan range
} scandata;                  // Scan settings

#seekto 0x81A0;
struct {
    u8 bit0:1, bit1:1, bit2:1, bit3:1, bit4:1, bit5:1, bit6:1, bit7:1;
} scanadd[80];               // Scan add flags (80 bytes × 8 bits = 640 channels)

#seekto 0x8200;
struct {
  u8 DtmfSw;                 // DTMF switch
  u8 CodeSpeed;              // Code speed
  u8 FirstCodeTim;           // First code time in milliseconds
  u8 PreTime;                // Pre-time in milliseconds
  u8 CodeDly;                // Code delay in milliseconds
  u8 PttIDPause;             // PTT ID pause
  u8 DtmfTone;               // DTMF tone
  u8 ResetTime;              // Reset time
  u8 SepCode;                // Separator code
  u8 GrpCode;                // Group code
  u8 DecRsp;                 // Decode response
  u8 padding[5];             // Padding
  char Did[3];               // Device ID (3 characters)
  u8 padding2[5];            // Padding
  char Bot[16];              // Beginning of transmission (16 characters)
  char Eot[16];              // End of transmission (16 characters)
  char Stun[16];             // Stun code (16 characters)
  char Kill[16];             // Kill code (16 characters)
} DtmfSysInfo;               // DTMF system information

struct {
    u16 useflg;              // Combined flag for 16 entries (0=valid, 1=invalid)
    u8 reserved[6];          // Reserved
} DtmfUseFlag;               // DTMF use flags

struct {
    char EncCode[16];        // DTMF encoding code (16 characters)
} DtmfEncTab[16];            // DTMF encoding table (16 entries)

#seekto 0x8400;
struct {
    u8 FirstTone;            // First tone
    u8 SecondTone;           // Second tone
    u8 ToneDur;              // Tone duration
    u8 ToneInt;              // Tone interval
    u8 SToneSW;              // Single tone switch
    u8 reserved[3];          // Reserved
} TwoToneInfo;               // Two-tone information

#seekto 0x8410;
struct {
    u16 freq1;               // First frequency
    u16 freq2;               // Second frequency
    char name[12];           // Name
} TwoToneEncList[16];        // Two-tone encoding list (16 entries)

struct {
    u8 DecodeRsp;            // Decode response
    u8 ReseTim;              // Reset time
    u8 DecFormat;            // Decode format
    u8 reserved;             // Reserved
    u16 Atone;               // A tone
    u16 Btone;               // B tone
    u16 Ctone;               // C tone
    u16 Dtone;               // D tone
    u8 reserved2[4];         // Reserved
} TwoToneDec;                // Two-tone decoding

#seekto 0x8680;
struct {
    u8 EncStand;             // Encoding standard
    u8 EncCodeTim;           // Encoding code time
    u8 EncCodeLen;           // Encoding code length
    u8 EncID[20];            // Encoding ID
    u8 EncScall;             // Encoding Scall
    char EncName[8];         // Encoding name
} FiveToneEncList[104];      // Five-tone encoding list (104 entries)

struct {
    u8 TblEn[13];            // 13 bytes covering 104 entries (104 bits)
} FiveToneEncFlag;           // Five-tone encoding flags

#seekto 0x9400;
struct {
    u8 PidStandS;            // PTT ID start standard
    u8 PidCodeTimS;          // PTT ID start code time
    u8 PidCodeLenS;          // PTT ID start code length
    u8 PidStart[12];         // PTT ID start code
    u8 reserved;             // Reserved
    u8 PidStandE;            // PTT ID end standard
    u8 PidCodeTimE;          // PTT ID end code time
    u8 PidCodeLenE;          // PTT ID end code length
    u8 PidEnd[12];           // PTT ID end code
    u8 reserved2;            // Reserved
} FiveToneEncPTTID;          // Five-tone encoding PTT ID

#seekto 0x9440;
struct {
    u8 DecRsp;               // Decode response
    u8 DecStand;             // Decode standard
    u8 DecToneTim;           // Decode tone time
    u8 Did[5];               // Device ID
    u8 reserved[3];          // Reserved
    u8 PreTime;              // Pre time
    u8 CodeDly;              // Code delay
    u8 PttIDPause;           // PTT ID pause
    u8 ResetTime;            // Reset time
    u8 FirstCodeTim;         // First code time
    u8 FiveAni;              // Five ANI
    u8 reserved2;            // Reserved
    u8 StopCode;             // Stop code
    u8 StopCodetime;         // Stop code time
    u8 DecCodetime;          // Decode code time
    u8 reserved3[11];        // Reserved
} FiveToneDec;               // Five-tone decoding

#seekto 0x9480;
struct {
    u8 Func;                 // Function
    u8 RspInfo;              // Response info
    u8 CdLen;                // Code length
    u8 DecID[6];             // Decode ID
    u8 reserved;             // Reserved
    char DecName[6];         // Decode name
} FiveToneInfoCd[8];         // Five-tone info codes (8 entries)

#seekto 0x9580;
struct {
    u8 SysList[8];           // System list
} MdcDecInfo;                // MDC decode info

struct {
    u8 CtrlSw:1,             // Control switch
       DecTone:1,            // Decode tone
       reserved:6;           // Reserved
    u16 EncID;               // Encode ID
    u8 PreTim;               // Pre time
    u8 SqlDly;               // Squelch delay
    u8 DecRst;               // Decode reset
    u8 EncSync;              // Encode sync
    u8 DecSync;              // Decode sync
} MdcPara[5];                // MDC parameters (5 entries)

struct {
    u16 BotTime;             // Beginning of transmission time
    u16 EotTime;             // End of transmission time
    u8 EncEn:1,              // Encode enable
       DecEn:1,              // Decode enable
       BotEn:1,              // BOT enable
       EotEn:1,              // EOT enable
       TxTone:1,             // TX tone
       RxTone:1,             // RX tone
       reserved_bits:2;      // Reserved
    u8 reserved_bytes[3];    // Reserved
} MdcPttID[5];               // MDC PTT ID (5 entries)

struct {
    u8 TblEn[16];            // 16 bytes covering 128 entries (128 bits)
} MdcTblEn;                  // MDC table enable flags

#seekto 0x95F0;
struct {
    u16 DecID;               // Decode ID
    u8 DecRsp;               // Decode response
    u8 reserved;             // Reserved
    char DecName[12];        // Decode name
} MdcDecList[100];           // MDC decode list (100 entries)

#seekto 0x9C80;
struct {
    u16 SelfID;              // Self ID
    u16 GrpID;               // Group ID
    u16 Sync;                // Sync
    u8 ZoneCode;             // Zone code
    u8 PreTime;              // Pre time
    u8 ToneSw;               // Tone switch
    u8 reserved[7];          // Reserved
} MdcBiis;                   // MDC BIIS

#seekto 0x9D00;
struct {
    u8 reserved[8];          // Reserved
} EmergHeader;               // Emergency header

struct {
    u8 Duration;             // Duration
    u8 ChSel;                // Channel selection
    u8 RxTime;               // RX time
    u8 TxTime;               // TX time
    u8 ExgTime;              // Exchange time
    u8 GrpNo;                // Group number
    u8 Mode;                 // Mode
    u8 Type;                 // Type
    u8 reserved[6];          // Reserved
    u8 Chn;                  // Channel
    u8 Zone;                 // Zone
} EmergInfo[8];              // Emergency information (8 entries)

#seekto 0x9E00;
struct {
    char DesNo[6];           // Destination callsign
    u8 DesID;                // Destination SSID
    u8 PacketFlags;          // Packet flags bitfield (passall:1,position:1,mice:1,object:1,item:1,message:1,wxreport:1,nmeareport:1)
    char SrcNo[6];           // Source callsign
    u8 SrcID;                // Source SSID
    u8 StatusFlags;          // Status flags (statusreport:1,other:1,power:2,band:1,beeptone:1,longdir:1,latdir:1)
    u8 PreTime;              // Pre time
    u8 CodeDly;              // Code delay
    u8 CtdcsH;               // CTCSS/DCS high byte
    u8 CtdcsL;               // CTCSS/DCS low byte
    char PositionTable;      // Position symbol table
    char PositionIcon;       // Position symbol icon
    u8 RxCallsignNum;        // RX callsign number
    u8 CallSignTotal;        // Call sign total
    struct {
        char CallSign[6];    // Callsign
        u8 ID;               // ID
        u8 reserved;         // Reserved
    } CallSigns[8];          // Call signs (8 entries)
    u8 SendInterval;         // Send interval
    u8 RegularlySend;        // Regularly send
    u8 AprsDisplayTime;      // APRS display time
    u8 reserved1;            // Reserved
    u8 ConfigFlags;          // Config flags (mice_type:3,ptt_id:2,height_type:1,beacon:1,reserved2:1)
    u8 reserved3[2];         // Reserved
    u8 TxtLength;            // Text length
    i32 Longitude;           // Longitude (scaled by 100000)
    i32 Latitude;            // Latitude (scaled by 100000)
    i32 Height;              // Height
    char Text[60];           // Text message
    u32 Freq[8];             // Frequencies (8 frequencies)
    struct {
        char CallSign[6];    // Callsign
        u8 ID;               // ID
        u8 Filter;           // Filter
    } RxCallSigns[32];       // RX callsigns (32 entries)
} AprsSet;                   // APRS settings

#seekto 0xA000;
struct {
    u8 flag[10];             // 10 bytes covering 80 entries (80 bits)
} GpsBookValidFlag;          // GPS book valid flags

#seekto 0xA010;
struct {
    u8 CodeID;               // Code ID
    u8 reserved;             // Reserved
    char CodeName[14];       // Code name
} GpsBook[100];              // GPS book entries (100 entries)
"""

# Power levels
POWER_LEVELS = [
    chirp_common.PowerLevel("Low", watts=2.0),
    chirp_common.PowerLevel("Medium", watts=5.0),
    chirp_common.PowerLevel("High", watts=10.0)
]

# Lists for settings
BACKLIGHT_LIST = ["Off", "5 Sec", "10 Sec", "15 Sec", "20 Sec", "30 Sec", 
                 "40 Sec", "50 Sec", "1 Min", "2 Min", "3 Min", "Always"]
VOX_LIST = ["Off"] + [str(x) for x in range(1, 10)]
BUSYLOCK_LIST = ["Off", "Carrier", "QT/DQT"]
VOICE_LIST = ["Off", "Chinese", "English"]
LANGUAGE_LIST = ["English", "Chinese"]
STEP_LIST = ["2.5K", "5.0K", "6.25K", "10.0K", "12.5K", "25.0K", "50.0K", "100.0K"]
TIMEOUT_LIST = ["Off", "15 sec", "30 sec", "60 sec", "90 sec", "120 sec", "150 sec", 
                "180 sec", "210 sec"]
DISPLAY_ORIENTATION_LIST = ["Standard", "Reverse"]

@directory.register
class BF5RHPRORadio(chirp_common.CloneModeRadio):
    """Baofeng 5RHPRO"""
    VENDOR = "Baofeng"
    MODEL = "5RHPRO"
    BAUD_RATE = 115200
    NEEDS_COMPAT_SERIAL = False
    
    def get_features(self):
        rf = chirp_common.RadioFeatures()
        rf.has_settings = True
        rf.has_bank = False
        rf.has_ctone = True
        rf.has_cross = True
        rf.has_rx_dtcs = True
        rf.has_tuning_step = False
        rf.has_name = True
        rf.can_odd_split = True
        rf.valid_name_length = 16
        rf.valid_characters = chirp_common.CHARSET_ASCII
        rf.valid_duplexes = ["", "-", "+", "split", "off"]
        rf.valid_tmodes = ["", "Tone", "TSQL", "DTCS", "Cross"]
        if hasattr(self, 'is_k6') and self.is_k6:
            # K6 model-specific features
            rf.valid_bands = [(136000000, 174000000), (400000000, 470000000)]
        else:
            # Standard model
            rf.valid_bands = [(136000000, 174000000), (200000000, 260000000), 
                              (350000000, 399000000), (400000000, 520000000)]
        rf.valid_skips = ["", "S"]
        rf.valid_modes = ["FM", "NFM"]
        rf.valid_power_levels = POWER_LEVELS
        rf.memory_bounds = (1, 640)

        return rf

    def sync_in(self):
        """Download from radio"""
        self.pipe.timeout = 1
        try:
            self._mmap = bf5rhpro_download(self)
            self.process_mmap()
        except Exception as e:
            raise errors.RadioError(f"Error downloading from radio: {e}")

    def log_zone_contents(self):
        """Log the contents of all zones for debugging"""
        LOG.debug("==== ZONE INFORMATION ====")
        for z in range(self._memobj.zones.zone_total):
            zone = self._memobj.zones.zone_info[z]
            zone_name = ""
            try:
                raw_name = bytes([int(x) for x in zone.zone_name])
                name_end = raw_name.find(b'\x00')
                if name_end != -1:
                    raw_name = raw_name[:name_end]
                zone_name = raw_name.decode('GB2312', errors='replace').rstrip()
            except Exception:
                zone_name = f"Zone {z+1}"

            LOG.debug(f"Zone {z} ({zone_name}): {zone.chn_num} channels")
            channel_list = []
            for i in range(zone.chn_num):
                channel_id = zone.chn_id[i]
                valid = self._is_channel_valid(channel_id)
                channel_list.append(f"{channel_id}{'✓' if valid else '✗'}")
            LOG.debug(f"  Channels: {', '.join(channel_list)}")

    def sync_out(self):
        """Upload to radio"""
        self.pipe.timeout = 1
        try:
            # Pre-upload validation
            # Ensure all zones have channels in correct order
            self.ensure_correct_zone_ordering()
            
            # Ensure all valid channels are in their correct zones
            for channel_num in range(1, MAX_CHN_NUM + 1):
                if self._is_channel_valid(channel_num):
                    target_zone = (channel_num - 1) // ZONE_MAX_CHN_NUM
                    if target_zone < ZONE_MAX_NUM:
                        zone = self._memobj.zones.zone_info[target_zone]
                        # Check if channel is in its target zone
                        found_in_zone = False
                        for i in range(zone.chn_num):
                            if zone.chn_id[i] == channel_num-1:
                                found_in_zone = True
                                break
                        # If not found, add it to the correct zone
                        if not found_in_zone:
                            self.add_channel_to_zone(target_zone, channel_num)

            # Second pass: ensure all zones only reference valid channels
            for zone_idx in range(self._memobj.zones.zone_total):
                zone = self._memobj.zones.zone_info[zone_idx]
                i = 0
                while i < zone.chn_num:
                    channel_idx = zone.chn_id[i]  # This is a 0-based index
                    # Convert from 0-based to 1-based for validation
                    if channel_idx < 0 or not self._is_channel_valid(channel_idx + 1):
                        # Remove invalid channel from zone (using 1-based channel number)
                        self.remove_channel_from_zone(zone_idx, channel_idx + 1)
                        # Don't increment i since elements were shifted
                    else:
                        i += 1

            # Call the upload function
            bf5rhpro_upload(self)
        except Exception as e:
            raise errors.RadioError(f"Error uploading to radio: {e}")

    def process_mmap(self):
        """Process the memory map into a usable object"""
        self._memobj = bitwise.parse(MEM_FORMAT, self._mmap)

    def _decode_tone(self, tone_h, tone_l):
        """Decode CTCSS/DCS tone values with proper endianness"""
        # Convert the bitwise elements to integers
        tone_h = int(tone_h)
        tone_l = int(tone_l)

        if tone_h == 0 and tone_l == 0:
            return "", None, None

        # Check for DCS by looking at highest bit of tone_h
        if tone_h & 0x80:
            # This is a DCS code
            # The DCS code appears to be stored in BCD format (Binary Coded Decimal)
            # Extract the actual DCS code bits
            hundreds = tone_h & 0x07
            tens = (tone_l >> 4) & 0x0F
            ones = tone_l & 0x0F

            # Combine to get the actual DCS code
            code = hundreds * 100 + tens * 10 + ones

            # Check if it's in the valid DCS codes list
            if code not in chirp_common.DTCS_CODES:
                # Find the closest valid DCS code
                valid_codes = sorted(chirp_common.DTCS_CODES)
                closest_idx = min(range(len(valid_codes)), 
                                 key=lambda i: abs(valid_codes[i] - code))
                code = valid_codes[closest_idx]
                LOG.warning(f"Invalid DCS code {hundreds * 100 + tens * 10 + ones} " +
                           f"replaced with closest valid code {code}")

            polarity = "I" if tone_h & 0x40 else "N"
            return "DTCS", code, polarity
        else:
            # This is a CTCSS tone
            # CTCSS tone is stored as (tone_h = hundreds/tens, tone_l = ones/decimal)
            hundreds = (tone_h & 0xF0) >> 4
            tens = tone_h & 0x0F
            ones = (tone_l & 0xF0) >> 4
            decimal = tone_l & 0x0F

            tone_value = hundreds * 100 + tens * 10 + ones + decimal * 0.1
            return "Tone", tone_value, None

    def _encode_tone(self, mode, value, pol=None):
        """Encode CTCSS/DCS tone values with proper endianness"""
        if mode == "":
            return 0, 0

        elif mode == "Tone" or mode == "TSQL":
            # For CTCSS tones like 162.2, we need to encode as:
            # tone_h = 0x16 (1 in hundreds place, 6 in tens place)
            # tone_l = 0x22 (2 in ones place, 2 in decimal place)

            # Split the tone value into its components
            tone_value = float(value)
            hundreds = int(tone_value / 100)
            tens = int((tone_value % 100) / 10)
            ones = int(tone_value % 10)
            decimal = int((tone_value * 10) % 10)

            tone_h = (hundreds << 4) | tens
            tone_l = (ones << 4) | decimal
            return tone_h, tone_l

        elif mode == "DTCS":
            # For DCS codes like 315
            code = int(value)

            # Extract BCD digits
            hundreds = code // 100
            tens = (code % 100) // 10
            ones = code % 10

            # Encode as BCD
            tone_h = 0x80 | (hundreds & 0x07)  # 0x80 = DCS flag
            tone_l = (tens << 4) | ones

            if pol == "I":
                tone_h |= 0x40  # Inverted flag

            return tone_h, tone_l

        return 0, 0

    def _is_channel_valid(self, number):
        """Check if a channel is marked as valid in the chnvalid array using 64-bit values"""
        # Ensure channel number is within valid range
        if number < 1 or number > MAX_CHN_NUM:
            return False

        # Each chnvalid element has a single 64-bit value
        array_idx = (number - 1) // 64

        # If the index is out of range (for channels > 640)
        if array_idx >= len(self._memobj.chnvalid):
            return False

        # Calculate which bit within the 64-bit value
        bit_pos = (number - 1) % 64

        # In the Baofeng format, 0 = valid, 1 = invalid
        # Check if the specific bit is 0 (valid)
        return not (int(self._memobj.chnvalid[array_idx].flags) & (1 << bit_pos))

    def _set_channel_valid(self, number, valid):
        """Mark a channel as valid or invalid in the chnvalid array using 64-bit values"""
        # Each chnvalid element has a single 64-bit value
        array_idx = (number - 1) // 64
    
        # If the index is out of range (for channels > 640)
        if array_idx >= len(self._memobj.chnvalid):
            return
    
        # Calculate which bit within the 64-bit value
        bit_pos = (number - 1) % 64
    
        # Get current flags value
        flags = int(self._memobj.chnvalid[array_idx].flags)
    
        # Set or clear the bit (0 = valid, 1 = invalid)
        if valid:
            # Clear the bit (make valid)
            flags &= ~(1 << bit_pos)
        else:
            # Set the bit (make invalid)
            flags |= (1 << bit_pos)
    
        # Update the flags value
        self._memobj.chnvalid[array_idx].flags = flags
    
    def _bcd_decode_freq(self, bcd_data):
        """Decode frequency with proper endianness"""
        # Convert to int first
        bcd_data = int(bcd_data)

        if bcd_data == 0xFFFFFFFF:
            return 0

        # The bytes are actually stored in REVERSED order
        # For 0x00251544 (which represents 441.52500 MHz):
        # We need to swap the byte order: 0x44152500

        # First, extract each byte in the original order
        b4 = bcd_data & 0xFF           # 0x44
        b3 = (bcd_data >> 8) & 0xFF    # 0x15
        b2 = (bcd_data >> 16) & 0xFF   # 0x25
        b1 = (bcd_data >> 24) & 0xFF   # 0x00

        # Swap the byte order
        swapped_data = (b4 << 24) | (b3 << 16) | (b2 << 8) | b1

        # Now extract BCD digits from the swapped data
        b1 = (swapped_data >> 24) & 0xFF  # MSB (0x44)
        b2 = (swapped_data >> 16) & 0xFF  # (0x15)
        b3 = (swapped_data >> 8) & 0xFF   # (0x25)
        b4 = swapped_data & 0xFF          # LSB (0x00)

        # Convert each BCD nibble to decimal
        d1 = (b1 >> 4) & 0x0F  # 4 (hundreds MHz)
        d2 = b1 & 0x0F         # 4 (tens MHz)
        d3 = (b2 >> 4) & 0x0F  # 1 (ones MHz)
        d4 = b2 & 0x0F         # 5 (tenths MHz)
        d5 = (b3 >> 4) & 0x0F  # 2 (hundredths MHz)
        d6 = b3 & 0x0F         # 5 (thousandths MHz)
        d7 = (b4 >> 4) & 0x0F  # 0 (ten-thousandths MHz)
        d8 = b4 & 0x0F         # 0 (hundred-thousandths MHz)

        # Combine into frequency in Hz
        freq = (d1*100 + d2*10 + d3) * 1000000 + \
               (d4*100 + d5*10 + d6) * 1000 + \
               (d7*10 + d8) * 10

        return freq
    
    def _bcd_encode_freq(self, freq_hz):
        """Encode frequency in BCD format with correct endianness"""
        if freq_hz == 0:
            return 0xFFFFFFFF

        # For 441.52500 MHz, needs to be encoded as 0x00251544

        # Extract digits
        freq_mhz = freq_hz / 1000000
        int_part = int(freq_mhz)
        frac_part = int(round((freq_mhz - int_part) * 100000))

        # Extract digits
        d1 = (int_part // 100) % 10     # 4 (hundreds MHz)
        d2 = (int_part // 10) % 10      # 4 (tens MHz)
        d3 = int_part % 10              # 1 (ones MHz)
        d4 = (frac_part // 10000) % 10  # 5 (tenths MHz)
        d5 = (frac_part // 1000) % 10   # 2 (hundredths MHz)
        d6 = (frac_part // 100) % 10    # 5 (thousandths MHz)
        d7 = (frac_part // 10) % 10     # 0 (ten-thousandths MHz)
        d8 = frac_part % 10             # 0 (hundred-thousandths MHz)

        # Combine into BCD bytes in normal order
        b1 = (d1 << 4) | d2           # 0x44
        b2 = (d3 << 4) | d4           # 0x15
        b3 = (d5 << 4) | d6           # 0x25
        b4 = (d7 << 4) | d8           # 0x00

        # Now swap the byte order for storage
        swapped_value = (b4 << 24) | (b3 << 16) | (b2 << 8) | b1

        return swapped_value

    def get_memory(self, number):
        """Get a memory object for the specified channel number"""
        mem = chirp_common.Memory()
        mem.number = number

        # Check if this memory is empty
        if not self._is_channel_valid(number):
            mem.empty = True
            return mem

        _mem = self._memobj.memory[number-1]

        # Extract name - handling Chinese character encoding
        try:
            # Convert from bitwise charDataElement to bytes first
            raw_name = bytes([int(x) for x in _mem.chn_name])
            name_end = raw_name.find(b'\x00')
            if name_end != -1:
                raw_name = raw_name[:name_end]

            # Decode with GB2312, falling back to ASCII if there's an issue
            name = raw_name.decode('GB2312', errors='replace').rstrip()
            mem.name = name
        except Exception as e:
            LOG.warning(f"Error decoding name for channel {number}: {e}")
            mem.name = ""

        # Extract frequency data with proper BCD decoding
        mem.freq = self._bcd_decode_freq(int(_mem.rx_freq))
        tx_freq = self._bcd_decode_freq(int(_mem.tx_freq))

        # Handle duplex/offset
        if _mem.tx_freq == 0xFFFFFFFF:
            mem.duplex = "off"
            mem.offset = 0
        elif mem.freq == tx_freq:
            mem.duplex = ""
            mem.offset = 0
        elif abs(mem.freq - tx_freq) > 70000000:  # 70MHz difference indicates split
            mem.duplex = "split"
            mem.offset = tx_freq
        else:
            if _mem.offsetdir == 2:
                mem.duplex = "-"
                mem.offset = mem.freq - tx_freq
            elif _mem.offsetdir == 1:
                mem.duplex = "+"
                mem.offset = tx_freq - mem.freq
            else:
                mem.duplex = ""
                mem.offset = 0

        # Extract mode (FM/NFM)
        mem.mode = "NFM" if _mem.wideth == 0 else "FM"

        # Handle power level
        if 0 <= _mem.power < len(POWER_LEVELS):
            mem.power = POWER_LEVELS[_mem.power]

        # Handle CTCSS/DCS tones with proper decoding
        rxmode, rxtone, rxpol = self._decode_tone(_mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l)
        txmode, txtone, txpol = self._decode_tone(_mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l)

        # Set tone modes
        if rxmode == "" and txmode == "":
            # No tone
            mem.tmode = ""
        elif rxmode == "" and txmode == "Tone":
            # Output tone only
            mem.tmode = "Tone"
            mem.rtone = txtone
        elif rxmode == "Tone" and txmode == "Tone" and rxtone == txtone:
            # Input and output tone, same frequency
            mem.tmode = "TSQL"
            mem.ctone = rxtone
            mem.rtone = txtone
        elif rxmode == "DTCS" and txmode == "DTCS" and rxtone == txtone and rxpol == txpol:
            # Input and output DTCS, same code and polarity
            mem.tmode = "DTCS"
            mem.dtcs = rxtone
            mem.dtcs_polarity = f"{rxpol}{txpol}"
        else:
            # Everything else is Cross
            mem.tmode = "Cross"

            if rxmode == "Tone" and txmode == "Tone":
                mem.cross_mode = "Tone->Tone"
                mem.ctone = rxtone
                mem.rtone = txtone
            elif rxmode == "DTCS" and txmode == "Tone":
                mem.cross_mode = "DTCS->Tone"
                mem.dtcs = rxtone
                mem.rtone = txtone
                mem.dtcs_polarity = f"{rxpol}N"
            elif rxmode == "Tone" and txmode == "DTCS":
                mem.cross_mode = "Tone->DTCS"
                mem.ctone = rxtone
                mem.dtcs = txtone
                mem.dtcs_polarity = f"N{txpol}"
            elif rxmode == "DTCS" and txmode == "DTCS":
                mem.cross_mode = "DTCS->DTCS"
                mem.rx_dtcs = rxtone
                mem.dtcs = txtone
                mem.dtcs_polarity = f"{rxpol}{txpol}"
            else:
                # Default to no tone if we can't determine the type
                LOG.warning(f"Unknown tone mode: RX={rxmode}:{rxtone}, TX={txmode}:{txtone}")
                mem.tmode = ""

        # Add extra settings for channel-specific parameters
        mem.extra = RadioSettingGroup("extra", "Extra Settings")

        # DTMF Index
        rs = RadioSetting("dtmf_idx", "DTMF Index",
                         RadioSettingValueInteger(0, 15, _mem.dtmf_idx))
        rs.set_doc("DTMF contact index (0-15)")
        mem.extra.append(rs)

        # Two Tone Index
        rs = RadioSetting("twotone_idx", "Two-Tone Index",
                         RadioSettingValueInteger(0, 15, _mem.twotone_idx))
        rs.set_doc("Two-tone signaling index (0-15)")
        mem.extra.append(rs)

        # Five Tone Index
        rs = RadioSetting("fivetone_idx", "Five-Tone Index",
                         RadioSettingValueInteger(0, 103, min(_mem.fivetone_idx, 103)))
        rs.set_doc("Five-tone signaling index (0-103)")
        mem.extra.append(rs)

        # MDC Index
        rs = RadioSetting("mdc_idx", "MDC Index",
                         RadioSettingValueInteger(0, 99, min(_mem.mdc_idx, 99)))
        rs.set_doc("MDC signaling index (0-99)")
        mem.extra.append(rs)

        # Scan List
        scanlist_val = _mem.scanlist if 0 <= _mem.scanlist <= MAX_SCAN_LIST_NUM else 0
        rs = RadioSetting("scanlist", "Scan List", 
                          RadioSettingValueInteger(0, MAX_SCAN_LIST_NUM, scanlist_val))
        rs.set_doc(f"Scan list assignment (0=None, 1-{MAX_SCAN_LIST_NUM})")
        mem.extra.append(rs)

        # Emergency System
        emerglist_val = _mem.emerglist if 0 <= _mem.emerglist <= MAX_EMERG_SYS_NUM else 0
        rs = RadioSetting("emerglist", "Emergency System",
                          RadioSettingValueInteger(0, MAX_EMERG_SYS_NUM, emerglist_val))
        rs.set_doc(f"Emergency system assignment (0=None, 1-{MAX_EMERG_SYS_NUM}=System number)")
        mem.extra.append(rs)

        # Scrambler
        rs = RadioSetting("scram", "Scrambler",
                         RadioSettingValueBoolean(_mem.scram))
        rs.set_doc("Enable voice scrambler for this channel")
        mem.extra.append(rs)

        # Compander
        rs = RadioSetting("compand", "Compander",
                         RadioSettingValueBoolean(_mem.compand))
        rs.set_doc("Enable audio compander for this channel")
        mem.extra.append(rs)

        # CTCSS/DCS Encryption
        rs = RadioSetting("cepin_dcs", "CTCSS/DCS Encryption",
                         RadioSettingValueBoolean(_mem.cepin_dcs))
        rs.set_doc("Enable CTCSS/DCS encryption for this channel")
        mem.extra.append(rs)

        # 24-bit Encryption
        rs = RadioSetting("cepin_24bit", "24-bit Encryption",
                         RadioSettingValueBoolean(_mem.cepin_24bit))
        rs.set_doc("Enable 24-bit encryption for this channel")
        mem.extra.append(rs)

        # TX Disable
        rs = RadioSetting("txdis", "TX Disable",
                         RadioSettingValueBoolean(_mem.txdis))
        rs.set_doc("Disable transmitting on this channel")
        mem.extra.append(rs)

        # Frequency Invert
        rs = RadioSetting("freqinvert", "Frequency Inversion",
                         RadioSettingValueBoolean(_mem.freqinvert))
        rs.set_doc("Enable frequency inversion")
        mem.extra.append(rs)

        # Talkaround
        rs = RadioSetting("talkaround", "Talkaround",
                         RadioSettingValueBoolean(_mem.talkaround))
        rs.set_doc("Enable talkaround (TX=RX)")
        mem.extra.append(rs)

        # Busy Lock
        rs = RadioSetting("busylock", "Busy Channel Lockout",
                          RadioSettingValueList(
                              ["Off", "Carrier"],
                              current_index=_mem.busylock))
        rs.set_doc("Busy channel lockout (prevents transmission when channel is busy)")
        mem.extra.append(rs)

        # Signal Type
        signal_types = ["Off", "DTMF", "2-Tone", "5-Tone", "MDC", "Digital"]
        if _mem.signaltype < len(signal_types):
            rs = RadioSetting("signaltype", "Signal Type",
                              RadioSettingValueList(
                                  signal_types,
                                  current_index=_mem.signaltype))
            rs.set_doc("Signaling system type")
            mem.extra.append(rs)        

        # Get zone information and add to comment field
        zone_idx = (number - 1) // ZONE_MAX_CHN_NUM
        if 0 <= zone_idx < ZONE_MAX_NUM and zone_idx < self._memobj.zones.zone_total:
            zone = self._memobj.zones.zone_info[zone_idx]
            try:
                # Get zone name
                raw_name = bytes([int(x) for x in zone.zone_name])
                name_end = raw_name.find(b'\x00')
                if name_end != -1:
                    raw_name = raw_name[:name_end]
                zone_name = raw_name.decode('GB2312', errors='replace').strip()
                if not zone_name:
                    zone_name = f"Zone {zone_idx+1}"
            except Exception:
                zone_name = f"Zone {zone_idx+1}"

            # Add zone name to comment field
            mem.comment = f"Zone: {zone_name}"
        else:
            LOG.debug(f"Channel {number} doesn't belong to a valid zone")

        return mem

    def get_mmap(self):
        """Return the memory map for this radio"""
        return self._mmap
    
    def save_mmap(self, filename):
        """Save the memory map to a file"""
        data = self._mmap.get_packed()
        if isinstance(data, str):
            data = data.encode('latin1')  # latin1 preserves byte values
        with open(filename, "wb") as f:
            f.write(data)

    def set_memory(self, mem):
        """Set a memory object to the radio memory map"""
        if mem.empty:
            # Mark channel as invalid
            self._set_channel_valid(mem.number, False)
    
            # Remove from all zones if the channel is deleted
            for zone_idx in range(self._memobj.zones.zone_total):
                self.remove_channel_from_zone(zone_idx, mem.number)
    
            # Clear the memory data when deleting
            _mem = self._memobj.memory[mem.number-1]
            
            # Reset all memory fields to default/empty values
            _mem.rx_freq = 0x00000000
            _mem.tx_freq = 0x00000000
            _mem.rx_ctcss_dcs_h = 0
            _mem.rx_ctcss_dcs_l = 0
            _mem.tx_ctcss_dcs_h = 0
            _mem.tx_ctcss_dcs_l = 0
            _mem.div_freq = 0
            _mem.power = 0
            _mem.wideth = 0
            _mem.offsetdir = 0
            _mem.dtmf_idx = 0
            _mem.twotone_idx = 0
            _mem.fivetone_idx = 0
            _mem.mdc_idx = 0
            _mem.scanlist = 0
            _mem.emerglist = 0
            _mem.chn_name = bytes([0x00] * 16)
    
            self.ensure_correct_zone_ordering()
            return
    
        # Mark channel as valid
        self._set_channel_valid(mem.number, True)
    
        # Get the memory object
        _mem = self._memobj.memory[mem.number-1]
    
        # Initialize with safe defaults if it's a new channel
        needs_init = (_mem.rx_freq == 0 or _mem.rx_freq == 0xFFFFFFFF)
        
        if needs_init:
            _mem.rx_freq = 0
            _mem.tx_freq = 0
            _mem.rx_ctcss_dcs_h = 0
            _mem.rx_ctcss_dcs_l = 0
            _mem.tx_ctcss_dcs_h = 0
            _mem.tx_ctcss_dcs_l = 0
            _mem.div_freq = 0
            _mem.power = 2  # High power
            _mem.wideth = 1  # Wide
            _mem.offsetdir = 0
            _mem.freqinvert = 0
            _mem.talkaround = 0
            _mem.fivetoneptt = 0
            _mem.dtmfptt = 0
            _mem.sqtype = 0
            _mem.signaltype = 0
            _mem.jumpfreq = 0
            _mem.busylock = 0
            _mem.txdis = 0
            _mem.scram = 0
            _mem.compand = 0
            _mem.cepin_dcs = 0
            _mem.cepin_24bit = 0
            _mem.freq_step = 0
            _mem.dtmf_idx = 0
            _mem.twotone_idx = 0
            _mem.fivetone_idx = 0
            _mem.mdc_idx = 0
            _mem.scanlist = 0
            _mem.emerglist = 0
            _mem.chn_name = bytes([0x00] * 16)
    
        # Set name
        if len(mem.name) > 0:
            name = mem.name.ljust(16, '\0')
            try:
                _mem.chn_name = bytes(name[:16], 'GB2312')
            except Exception:
                _mem.chn_name = bytes(name[:16], 'ascii', 'replace')
        else:
            _mem.chn_name = bytes([0x00] * 16)
    
        # Set frequencies
        _mem.rx_freq = self._bcd_encode_freq(mem.freq)
        if mem.duplex == "off":
            _mem.tx_freq = 0xFFFFFFFF
        elif mem.duplex == "split":
            _mem.tx_freq = self._bcd_encode_freq(mem.offset)
        elif mem.duplex == "+":
            _mem.tx_freq = self._bcd_encode_freq(mem.freq + mem.offset)
            _mem.offsetdir = 1
        elif mem.duplex == "-":
            _mem.tx_freq = self._bcd_encode_freq(mem.freq - mem.offset)
            _mem.offsetdir = 2
        else:
            _mem.tx_freq = _mem.rx_freq
            _mem.offsetdir = 0
    
        # Set mode (FM/NFM)
        _mem.wideth = 1 if mem.mode == "FM" else 0
    
        # Set tone modes
        if mem.tmode == "":
            _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = 0, 0
            _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = 0, 0
        elif mem.tmode == "Tone":
            _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = 0, 0
            _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("Tone", mem.rtone)
        elif mem.tmode == "TSQL":
            _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = self._encode_tone("Tone", mem.ctone)
            _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("Tone", mem.rtone)
        elif mem.tmode == "DTCS":
            _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = self._encode_tone("DTCS", mem.dtcs, mem.dtcs_polarity[0])
            _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("DTCS", mem.dtcs, mem.dtcs_polarity[1])
        elif mem.tmode == "Cross":
            if mem.cross_mode == "Tone->Tone":
                _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = self._encode_tone("Tone", mem.ctone)
                _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("Tone", mem.rtone)
            elif mem.cross_mode == "DTCS->Tone":
                _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = self._encode_tone("DTCS", mem.dtcs, mem.dtcs_polarity[0])
                _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("Tone", mem.rtone)
            elif mem.cross_mode == "Tone->DTCS":
                _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = self._encode_tone("Tone", mem.ctone)
                _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("DTCS", mem.dtcs, mem.dtcs_polarity[1])
            elif mem.cross_mode == "DTCS->DTCS":
                _mem.rx_ctcss_dcs_h, _mem.rx_ctcss_dcs_l = self._encode_tone("DTCS", mem.rx_dtcs, mem.dtcs_polarity[0])
                _mem.tx_ctcss_dcs_h, _mem.tx_ctcss_dcs_l = self._encode_tone("DTCS", mem.dtcs, mem.dtcs_polarity[1])
    
        # Set power level
        if mem.power:
            # Handle different types of power values
            if hasattr(mem.power, 'watts'):
                # It's a PowerLevel object
                watts = mem.power.watts
            elif isinstance(mem.power, (int, float)):
                # It's a direct number
                watts = float(mem.power)
            else:
                # If it's a string or unknown format, use default (High)
                watts = 10.0
                
            # Find the closest power level match
            levels = [2.0, 5.0, 10.0]  # Low, Medium, High
            which = min(range(len(levels)), key=lambda i: abs(levels[i] - watts))
            _mem.power = which
        else:
            _mem.power = 2  # Default to High
    
        # Handle extra settings
        for setting in mem.extra:
            setattr(_mem, setting.get_name(), setting.value)
    
        # Determine which zone this channel belongs to based on channel number
        target_zone = (mem.number - 1) // ZONE_MAX_CHN_NUM
        position_in_zone = (mem.number - 1) % ZONE_MAX_CHN_NUM
    
        # Ensure we don't exceed maximum number of zones
        if target_zone < ZONE_MAX_NUM:
            zone = self._memobj.zones.zone_info[target_zone]
    
            # Check if channel already exists in target zone
            found = False
            existing_position = -1
            for i in range(zone.chn_num):
                if zone.chn_id[i] == mem.number-1:
                    found = True
                    existing_position = i
                    break
                
            if found and existing_position != position_in_zone:
                # Channel exists but in wrong position - move it
                
                # Remove from current position
                for j in range(existing_position, zone.chn_num - 1):
                    zone.chn_id[j] = zone.chn_id[j + 1]
                zone.chn_num -= 1
    
                # Insert at correct position
                if position_in_zone < zone.chn_num:
                    # Shift channels to make room
                    for j in range(zone.chn_num, position_in_zone, -1):
                        zone.chn_id[j] = zone.chn_id[j - 1]
                    zone.chn_id[position_in_zone] = mem.number-1
                    zone.chn_num += 1
                else:
                    # Add at the end
                    zone.chn_id[zone.chn_num] = mem.number-1
                    zone.chn_num += 1
                    
            elif not found:
                # Channel doesn't exist in zone yet
                if zone.chn_num < ZONE_MAX_CHN_NUM:
                    if position_in_zone >= zone.chn_num:
                        # Add at the end
                        zone.chn_id[zone.chn_num] = mem.number-1
                        zone.chn_num += 1
                    else:
                        # Insert at specific position
                        for j in range(zone.chn_num, position_in_zone, -1):
                            zone.chn_id[j] = zone.chn_id[j - 1]
                        zone.chn_id[position_in_zone] = mem.number-1
                        zone.chn_num += 1
    
                    # Update the zone total count if needed
                    if target_zone + 1 > self._memobj.zones.zone_total:
                        self._memobj.zones.zone_total = target_zone + 1
    
                        # If this is a new zone, set a default name
                        if not any(zone.zone_name):
                            zone_name = f"Zone {target_zone + 1}"
                            try:
                                zone.zone_name = bytes(zone_name.ljust(16, '\0')[:16], 'GB2312')
                            except Exception:
                                zone.zone_name = bytes(zone_name.ljust(16, '\0')[:16], 'ascii', 'replace')
        
        # Ensure zones are properly ordered
        self.ensure_correct_zone_ordering()

    def get_raw_memory(self, number):
        """Return a raw representation of the memory"""
        if not self._is_channel_valid(number):
            return "Channel is marked as invalid"

        _mem = self._memobj.memory[number-1]
        s = ""
        
        # Format key fields
        s += f"RX Freq: 0x{int(_mem.rx_freq):08X} ({self._bcd_decode_freq(int(_mem.rx_freq))/1000000:.5f} MHz)\n"
        s += f"TX Freq: 0x{int(_mem.tx_freq):08X} ({self._bcd_decode_freq(int(_mem.tx_freq))/1000000:.5f} MHz)\n"
        s += f"RX tone: 0x{int(_mem.rx_ctcss_dcs_h):02X} 0x{int(_mem.rx_ctcss_dcs_l):02X}\n"
        s += f"TX tone: 0x{int(_mem.tx_ctcss_dcs_h):02X} 0x{int(_mem.tx_ctcss_dcs_l):02X}\n"

        # Decode name
        try:
            raw_name = bytes([int(x) for x in _mem.chn_name])
            name_end = raw_name.find(b'\x00')
            if name_end != -1:
                raw_name = raw_name[:name_end]
            name = raw_name.decode('GB2312', errors='replace').rstrip()
            s += f"Name: '{name}'\n"
        except Exception as e:
            s += f"Name: <decode error: {e}>\n"

        # Power and mode
        s += f"Power: {_mem.power} ({['Low', 'Medium', 'High'][_mem.power] if _mem.power < 3 else 'Invalid'})\n"
        s += f"Mode: {'NFM' if _mem.wideth == 0 else 'FM'}\n"

        # Signaling settings
        s += f"DTMF Index: {_mem.dtmf_idx}\n"
        s += f"Two-tone Index: {_mem.twotone_idx}\n"
        s += f"Five-tone Index: {_mem.fivetone_idx}\n"
        s += f"MDC Index: {_mem.mdc_idx}\n"
        s += f"Scan List: {_mem.scanlist}\n"

        # Advanced features
        s += f"Scrambler: {'On' if _mem.scram else 'Off'}\n"
        s += f"Compander: {'On' if _mem.compand else 'Off'}\n"
        s += f"TX Disable: {'Yes' if _mem.txdis else 'No'}\n"

        return s

    def get_zone_names(self):
        """Return a list of zone names"""
        zone_names = []
        for i in range(self._memobj.zones.zone_total):
            zone = self._memobj.zones.zone_info[i]
            try:
                raw_name = bytes([int(x) for x in zone.zone_name])
                name_end = raw_name.find(b'\x00')
                if name_end != -1:
                    raw_name = raw_name[:name_end]
                zone_name = raw_name.decode('GB2312', errors='replace').rstrip()
            except Exception:
                zone_name = f"Zone {i+1}"
            zone_names.append(zone_name)
        return zone_names

    def get_zone_channels(self, zone_index):
        """Return a list of channels in the specified zone"""
        if zone_index >= self._memobj.zones.zone_total:
            return []

        zone = self._memobj.zones.zone_info[zone_index]
        return [zone.chn_id[i] for i in range(zone.chn_num)]

    def move_channel_in_zone(self, zone_index, channel_number, new_position):
        """Move a channel to a new position within a zone"""
        if zone_index >= self._memobj.zones.zone_total:
            return False

        zone = self._memobj.zones.zone_info[zone_index]

        # Find current position
        current_pos = -1
        for i in range(zone.chn_num):
            if zone.chn_id[i] == channel_number:
                current_pos = i
                break
            
        if current_pos == -1:
            return False  # Channel not found

        # Validate new position
        if new_position < 0 or new_position >= zone.chn_num:
            return False

        # Move the channel by shifting entries
        if current_pos < new_position:
            # Moving right
            channel = zone.chn_id[current_pos]
            for i in range(current_pos, new_position):
                zone.chn_id[i] = zone.chn_id[i + 1]
            zone.chn_id[new_position] = channel
        elif current_pos > new_position:
            # Moving left
            channel = zone.chn_id[current_pos]
            for i in range(current_pos, new_position, -1):
                zone.chn_id[i] = zone.chn_id[i - 1]
            zone.chn_id[new_position] = channel

        return True

    def get_settings(self):
        """Get radio settings"""
        _settings = self._memobj.settings
        _dtmf = self._memobj.DtmfSysInfo
        _zones = self._memobj.zones
        _scan = self._memobj.scandata
        _vfos = self._memobj.vfo  # Access VFO structures

        # Create setting groups
        
        normal = RadioSettingGroup("normal", "Normal Settings")
        more = RadioSettingGroup("more", "More Settings")
        vfo_group = RadioSettingGroup("vfo", "VFO")  # Add new VFO group
        scan = RadioSettingGroup("scan", "Scan Settings")
        dtmf = RadioSettingGroup("dtmf", "DTMF Settings")
        zones_group = RadioSettingGroup("zones", "Zone Management")
        bluetooth = RadioSettingGroup("bluetooth", "Bluetooth Settings")

        # Main settings container
        group = RadioSettings(normal, more, vfo_group, scan, dtmf, zones_group, bluetooth)

        # Create sub-groups for VFO A and VFO B
        vfo_a = RadioSettingGroup("vfo_a", "VFO A")
        vfo_b = RadioSettingGroup("vfo_b", "VFO B")
        vfo_group.append(vfo_a)
        vfo_group.append(vfo_b)

        # Define all the options we'll need
        freq_step_options = ["2.5K", "5.0K", "6.25K", "8.33K", "10.0K", "12.5K", "20.0K", "25.0K", "30.0K", "50.0K", "100.0K"]
        freq_diff_dir_options = ["None", "+", "-"]
        bandwidth_options = ["12.5kHz", "25kHz"]
        power_options = ["Low", "Mid", "High"]
        signaling_options = ["None", "DTMF", "2Tone", "5Tone", "MDC", "BIS", "APRS"]
        ptt_id_options = ["OFF", "BOT", "EOT", "BOTH"]
        squelch_options = ["None", "CTDCS", "Optional", "CTDCS and Optional"]
        onoff_options = ["OFF", "ON"]
        emergency_options = ["None"] + [str(i) for i in range(1, 11)]

        # Build CTCSS/DCS options list
        tone_options = ["None"]
        # Add CTCSS tones
        for tone in chirp_common.TONES:
            tone_options.append(f"{tone:.1f}Hz")
        # Add DCS codes
        for code in chirp_common.DTCS_CODES:
            tone_options.append(f"D{code}N")
            tone_options.append(f"D{code}I")

        # Configure both VFOs
        for vfo_idx, vfo_panel in enumerate([vfo_a, vfo_b]):
            vfo = _vfos[vfo_idx]
            vfo_name = "A" if vfo_idx == 0 else "B"

            # RX Frequency
            rx_freq = self._bcd_decode_freq(int(vfo.rx_freq)) / 1000000.0
            rs = RadioSetting(f"vfo{vfo_idx}.rx_freq", f"RX Frequency [MHz]",
                             RadioSettingValueFloat(0, 999.999999, rx_freq, 6, 6))
            vfo_panel.append(rs)

            # TX Frequency
            tx_freq = self._bcd_decode_freq(int(vfo.tx_freq)) / 1000000.0
            rs = RadioSetting(f"vfo{vfo_idx}.tx_freq", f"TX Frequency [MHz]",
                             RadioSettingValueFloat(0, 999.999999, tx_freq, 6, 6))
            vfo_panel.append(rs)

            # Frequency Difference Direction
            offsetdir_idx = min(vfo.offsetdir, len(freq_diff_dir_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.offsetdir", f"Freq Diff Direction",
                             RadioSettingValueList(freq_diff_dir_options, current_index=offsetdir_idx))
            vfo_panel.append(rs)

            # Frequency Difference (calculated from rx and tx)
            if vfo.offsetdir == 0 or tx_freq == 0:
                freq_diff = 0
            else:
                freq_diff = abs(rx_freq - tx_freq)

            rs = RadioSetting(f"vfo{vfo_idx}.freq_diff", f"Frequency Difference [MHz]",
                             RadioSettingValueFloat(0, 999.999999, freq_diff, 6, 6))
            vfo_panel.append(rs)

            # Step
            step_idx = min(vfo.freq_step, len(freq_step_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.freq_step", f"Step [kHz]",
                             RadioSettingValueList(freq_step_options, current_index=step_idx))
            vfo_panel.append(rs)

            # CTCSS/DCS Decode
            rx_mode, rx_tone, rx_pol = self._decode_tone(vfo.rx_ctcss_dcs_h, vfo.rx_ctcss_dcs_l)
            rx_tone_str = "None"
            if rx_mode == "Tone":
                rx_tone_str = f"{rx_tone:.1f}Hz"
            elif rx_mode == "DTCS":
                rx_tone_str = f"D{rx_tone}{'I' if rx_pol == 'I' else 'N'}"

            idx = 0
            if rx_tone_str in tone_options:
                idx = tone_options.index(rx_tone_str)

            rs = RadioSetting(f"vfo{vfo_idx}.rx_tone", f"CTCSS/DCS Dec",
                             RadioSettingValueList(tone_options, current_index=idx))
            vfo_panel.append(rs)

            # CTCSS/DCS Encode
            tx_mode, tx_tone, tx_pol = self._decode_tone(vfo.tx_ctcss_dcs_h, vfo.tx_ctcss_dcs_l)
            tx_tone_str = "None"
            if tx_mode == "Tone":
                tx_tone_str = f"{tx_tone:.1f}Hz"
            elif tx_mode == "DTCS":
                tx_tone_str = f"D{tx_tone}{'I' if tx_pol == 'I' else 'N'}"

            idx = 0
            if tx_tone_str in tone_options:
                idx = tone_options.index(tx_tone_str)

            rs = RadioSetting(f"vfo{vfo_idx}.tx_tone", f"CTCSS/DCS Enc",
                             RadioSettingValueList(tone_options, current_index=idx))
            vfo_panel.append(rs)

            # Bandwidth
            bandwidth_idx = 1 if vfo.wideth == 1 else 0  # 0=narrow(12.5kHz), 1=wide(25kHz)
            rs = RadioSetting(f"vfo{vfo_idx}.wideth", f"Bandwidth",
                             RadioSettingValueList(bandwidth_options, current_index=bandwidth_idx))
            vfo_panel.append(rs)

            # Power
            power_idx = min(vfo.power, 2)
            rs = RadioSetting(f"vfo{vfo_idx}.power", f"Power",
                             RadioSettingValueList(power_options, current_index=power_idx))
            vfo_panel.append(rs)

            # Optional Signaling
            signal_idx = min(vfo.signaltype, len(signaling_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.signaltype", f"Optional Signaling",
                             RadioSettingValueList(signaling_options, current_index=signal_idx))
            vfo_panel.append(rs)

            # DTMF Index
            rs = RadioSetting(f"vfo{vfo_idx}.dtmf_idx", f"DTMF Index Number",
                             RadioSettingValueInteger(0, 15, vfo.dtmf_idx))
            vfo_panel.append(rs)

            # 2-Tone Index
            rs = RadioSetting(f"vfo{vfo_idx}.twotone_idx", f"2-Tone Index Number",
                             RadioSettingValueInteger(0, 15, vfo.twotone_idx))
            vfo_panel.append(rs)

            # 5-Tone Index
            rs = RadioSetting(f"vfo{vfo_idx}.fivetone_idx", f"5-Tone Index Number",
                             RadioSettingValueInteger(0, 103, vfo.fivetone_idx))
            vfo_panel.append(rs)

            # MDC Index
            rs = RadioSetting(f"vfo{vfo_idx}.mdc_idx", f"MDC Index Number",
                             RadioSettingValueInteger(0, 99, vfo.mdc_idx))
            vfo_panel.append(rs)

            # DTMF PTT ID
            dtmf_ptt_idx = min(vfo.dtmfptt, len(ptt_id_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.dtmfptt", f"DTMF PTT ID",
                             RadioSettingValueList(ptt_id_options, current_index=dtmf_ptt_idx))
            vfo_panel.append(rs)

            # 5-Tone PTT ID
            fivetone_ptt_idx = min(vfo.fivetoneptt, len(ptt_id_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.fivetoneptt", f"5-Tone PTT ID",
                             RadioSettingValueList(ptt_id_options, current_index=fivetone_ptt_idx))
            vfo_panel.append(rs)

            # Talkaround
            rs = RadioSetting(f"vfo{vfo_idx}.talkaround", f"Talkaround",
                             RadioSettingValueList(onoff_options, current_index=vfo.talkaround))
            vfo_panel.append(rs)

            # Squelch
            sqtype_idx = min(vfo.sqtype, len(squelch_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.sqtype", f"Squelch",
                             RadioSettingValueList(squelch_options, current_index=sqtype_idx))
            vfo_panel.append(rs)

            # Emergency System
            emerglist_val = vfo.emerglist if 0 <= vfo.emerglist <= MAX_EMERG_SYS_NUM else 0
            rs = RadioSetting(f"vfo{vfo_idx}.emerglist", f"Emergency System",
                             RadioSettingValueList(emergency_options, current_index=emerglist_val))
            vfo_panel.append(rs)

            # Launch Banned (TX Disable)
            txdis_idx = 1 if vfo.txdis else 0
            rs = RadioSetting(f"vfo{vfo_idx}.txdis", f"Launch Banned",
                             RadioSettingValueList(onoff_options, current_index=txdis_idx))
            vfo_panel.append(rs)

            # Jump Frequency
            jumpfreq_idx = min(vfo.jumpfreq, len(onoff_options) - 1)
            rs = RadioSetting(f"vfo{vfo_idx}.jumpfreq", f"JumpFreq",
                             RadioSettingValueList(onoff_options, current_index=jumpfreq_idx))
            vfo_panel.append(rs)

            # Inverted Frequency
            freqinvert_idx = 1 if vfo.freqinvert else 0
            rs = RadioSetting(f"vfo{vfo_idx}.freqinvert", f"Inverted Freq.",
                             RadioSettingValueList(onoff_options, current_index=freqinvert_idx))
            vfo_panel.append(rs)
        # Normal Settings
        # Band A Work Mode
        rs = RadioSetting("ch_a_mode", "Band A Work Mode",
                         RadioSettingValueList(
                             ["VFO", "MR"], 
                             current_index=_settings.ch_a_mode))
        normal.append(rs)

        # Band B Work Mode
        rs = RadioSetting("ch_b_mode", "Band B Work Mode",
                         RadioSettingValueList(
                             ["VFO", "MR"], 
                             current_index=_settings.ch_b_mode))
        normal.append(rs)

        # Band A Work Zone - 1-indexed for display
        rs = RadioSetting("ch_a_zone", "Band A Work Zone",
                         RadioSettingValueInteger(1, 10, _settings.ch_a_zone + 1))
        normal.append(rs)

        # Band B Work Zone - 1-indexed for display
        rs = RadioSetting("ch_b_zone", "Band B Work Zone",
                         RadioSettingValueInteger(1, 10, _settings.ch_b_zone + 1))
        normal.append(rs)

        # Band A Channel Display
        display_modes = ["Frequency", "Name", "Number", "Frequency + Name"]
        idx = min(_settings.dispa_mode, len(display_modes) - 1) if _settings.dispa_mode >= 0 else 0
        rs = RadioSetting("dispa_mode", "Band A Channel Display",
                         RadioSettingValueList(display_modes, current_index=idx))
        normal.append(rs)

        # Band B Channel Display
        idx = min(_settings.dispb_mode, len(display_modes) - 1) if _settings.dispb_mode >= 0 else 0
        rs = RadioSetting("dispb_mode", "Band B Channel Display",
                         RadioSettingValueList(display_modes, current_index=idx))
        normal.append(rs)

        # Double Waiting
        rs = RadioSetting("dual_mode", "Double Waiting",
                         RadioSettingValueList(
                             ["Off", "Dual Waiting", "Single Waiting"],
                             current_index=_settings.dual_mode))
        normal.append(rs)

        # Main Band
        rs = RadioSetting("main_band", "Main Band",
                         RadioSettingValueList(
                             ["A", "B"],
                             current_index=_settings.main_band))
        normal.append(rs)

        # For power-on password
        pw_str = ""
        for i in range(8):
            if int(_settings.pow_password[i]) >= ord('0') and int(_settings.pow_password[i]) <= ord('9'):
                pw_str += chr(int(_settings.pow_password[i]))
            elif int(_settings.pow_password[i]) != 0xFF:
                LOG.warning(f"Non-digit value 0x{int(_settings.pow_password[i]):02X} in password")
        try:
            pw_str = bytes([int(x) for x in _settings.pow_password]).decode('ascii', 'ignore').rstrip('\0 ')
        except:
            pw_str = ""

        rs = RadioSetting("pow_password", "Power On Password",
                         RadioSettingValueString(0, 8, pw_str))

        def apply_pw(setting, obj):
            pw = str(setting.value).strip()  # Strip whitespace
            password = bytearray(8)

            # Check if password contains ANY non-digit characters
            if pw and not all(char.isdigit() for char in pw):
                # Password contains invalid characters - reject it completely
                LOG.warning("ERROR: Password contains non-digit characters. Password rejected. Using all 0xFF.")

                # Fill with 0xFF (empty/unused values)
                for i in range(8):
                    password[i] = 0xFF

                # Set the password bytes in the radio object
                obj.pow_password = bytes(password)
        
                # Display a dialog to the user without raising an exception
                import wx
                dlg = wx.MessageDialog(None, 
                                     "Password contains non-digit characters.\nPassword rejected.",
                                     "Invalid Password", wx.OK | wx.ICON_WARNING)
                                     
                dlg.ShowModal()
                dlg.Destroy()
                
                # Schedule UI update to happen after this callback finishes
                wx.CallAfter(lambda: setting.value.set_value(""))
                return
            else:
                # Password is either empty or all digits - proceed normally
                # First fill with 0xFF (empty/unused values)
                for i in range(8):
                    password[i] = 0xFF

                # Then add valid digits
                for i in range(min(len(pw), 8)):
                    password[i] = ord(pw[i])

                obj.pow_password = bytes(password)


        rs.set_apply_callback(apply_pw, _settings)
        normal.append(rs)

        # Power On Screen
        rs = RadioSetting("PownFace", "Power On Screen",
                         RadioSettingValueList(
                             ["Picture", "Character", "Voltage"],
                             current_index=_settings.PownFace))
        normal.append(rs)

        # Power on Words (Radio Name)
        radio_name = ""
        try:
            radio_name = bytes([int(x) for x in _settings.radio_name]).decode('GB2312', 'ignore').rstrip('\0 ')
        except:
            radio_name = ""

        rs = RadioSetting("radio_name", "Power On Words",
                         RadioSettingValueString(0, 16, radio_name))

        def apply_radio_name(setting, obj):
            name = str(setting.value).ljust(16, '\0')
            try:
                obj.radio_name = bytes(name[:16], 'GB2312')
            except:
                obj.radio_name = bytes([0xFF] * 16)

        rs.set_apply_callback(apply_radio_name, _settings)
        normal.append(rs)

        # Back light Time
        backlight_options = ["Always", "5 Sec", "10 Sec", "15 Sec", "20 Sec", 
                            "25 Sec", "30 Sec"]
        idx = min(_settings.blight_time, len(backlight_options) - 1) if _settings.blight_time >= 0 else 0
        rs = RadioSetting("blight_time", "Back light Time",
                         RadioSettingValueList(backlight_options, current_index=idx))
        normal.append(rs)

        # Tone Level
        rs = RadioSetting("tone", "Tone Level",
                         RadioSettingValueInteger(1, 5, _settings.tone + 1))
        normal.append(rs)

        # For program password
        wr_pw_str = ""
        for i in range(8):
            if int(_settings.wr_password[i]) >= ord('0') and int(_settings.wr_password[i]) <= ord('9'):
                wr_pw_str += chr(int(_settings.wr_password[i]))
            elif int(_settings.wr_password[i]) != 0xFF:
                LOG.warning(f"Non-digit value 0x{int(_settings.wr_password[i]):02X} in program password")

        rs = RadioSetting("wr_password", "Program Password",
                         RadioSettingValueString(0, 8, wr_pw_str))

        def apply_wr_pw(setting, obj):
            pw = str(setting.value).strip()  # Strip whitespace
            password = bytearray(8)

            # Check if password contains ANY non-digit characters
            if pw and not all(char.isdigit() for char in pw):
                # Password contains invalid characters - reject it completely
                LOG.warning("ERROR: Program password contains non-digit characters. Password rejected. Using all 0xFF.")

                # Fill with 0xFF (empty/unused values)
                for i in range(8):
                    password[i] = 0xFF

                # Set the password bytes in the radio object
                obj.wr_password = bytes(password)

                # Display a dialog to the user without raising an exception
                # that would interrupt the settings update process
                import wx
                wx.MessageBox("Program password contains non-digit characters.\nPassword rejected.",
                              "Invalid Password", wx.OK | wx.ICON_WARNING)

                setting.value.set_value("")
                setting.value.set_mutable(True)
                # Return without error - UI will show original invalid text
                # but radio memory will have correct 0xFF values
                return
            else:
                # Password is either empty or all digits - proceed normally
                # First fill with 0xFF (empty/unused values)
                for i in range(8):
                    password[i] = 0xFF

                # Then add valid digits
                for i in range(min(len(pw), 8)):
                    password[i] = ord(pw[i])

                obj.wr_password = bytes(password)

        rs.set_apply_callback(apply_wr_pw, _settings)
        normal.append(rs)

        # Voice Announcements
        voice_options = ["Off", "Chinese", "English"]
        idx = min(_settings.voice, len(voice_options) - 1) if _settings.voice >= 0 else 0
        rs = RadioSetting("voice", "Voice Announcements",
                         RadioSettingValueList(voice_options, current_index=idx))
        normal.append(rs)

        # Squelch
        rs = RadioSetting("sqlv", "Squelch Level",
                         RadioSettingValueInteger(0, 9, _settings.sqlv))
        normal.append(rs)

        # Power Save
        power_save_options = ["OFF", "1:1", "1:2", "1:4"]
        idx = min(_settings.po_save, len(power_save_options) - 1) if _settings.po_save >= 0 else 0
        rs = RadioSetting("po_save", "Power Save",
                         RadioSettingValueList(power_save_options, current_index=idx))
        normal.append(rs)

        # Power Save Delay Times
        power_save_delay_options = ["5", "10", "15", "20", "25"]
        # Use integer division to map the stored value to our options
        idx = min(_settings.po_save_dly // 5, len(power_save_delay_options) - 1)
        rs = RadioSetting("po_save_dly", "Power Save Delay Times",
                         RadioSettingValueList(power_save_delay_options, current_index=idx))
        normal.append(rs)

        # Tone setting (missing in original)
        rs = RadioSetting("tone_switch", "Tone",
                         RadioSettingValueList(
                             ["OFF", "ON"],
                             current_index=_settings.tone))
        normal.append(rs)

        # Call End Tone
        call_end_tone_options = ["OFF", "Mode 1", "Mode 2", "Mode 3"]
        idx = min(_settings.endtone, len(call_end_tone_options) - 1) if _settings.endtone >= 0 else 0
        rs = RadioSetting("endtone", "Call End Tone",
                         RadioSettingValueList(call_end_tone_options, current_index=idx))
        normal.append(rs)

        # Language
        language_options = ["English", "Chinese"]
        idx = min(_settings.LangSel, len(language_options) - 1) if _settings.LangSel >= 0 else 0
        rs = RadioSetting("LangSel", "Language",
                         RadioSettingValueList(language_options, current_index=idx))
        normal.append(rs)

        # Tail
        tail_options = ["OFF", "55hz", "120°", "180°", "240°"]
        idx = min(_settings.TailFreq, len(tail_options) - 1) if _settings.TailFreq >= 0 else 0
        rs = RadioSetting("TailFreq", "Tail",
                         RadioSettingValueList(tail_options, current_index=idx))
        normal.append(rs)

        # Busy Lock
        busy_lock_options = ["OFF", "Carrier", "QT/DQT"]
        idx = min(_settings.busylock, len(busy_lock_options) - 1) if _settings.busylock >= 0 else 0
        rs = RadioSetting("busylock", "Busy Lock",
                         RadioSettingValueList(busy_lock_options, current_index=idx))
        normal.append(rs)

        # Auto Keypad Lock
        rs = RadioSetting("autokey", "Auto Keypad Lock",
                         RadioSettingValueList(
                             ["OFF", "ON"],
                             current_index=_settings.autokey))
        normal.append(rs)

        # APO[min]
        apo_options = ["OFF", "30", "60", "120", "240", "480"]
        # Map stored value to option index
        if _settings.apo == 0:
            idx = 0  # OFF
        elif _settings.apo == 30:
            idx = 1  # 30
        elif _settings.apo == 60:
            idx = 2  # 60
        elif _settings.apo == 120:
            idx = 3  # 120
        elif _settings.apo == 240:
            idx = 4  # 240
        elif _settings.apo == 480:
            idx = 5  # 480
        else:
            idx = 0  # Default to OFF if value is unknown

        rs = RadioSetting("apo", "APO[min]",
                         RadioSettingValueList(apo_options, current_index=idx))
        normal.append(rs)

        # Display Reverse
        rs = RadioSetting("DispDir", "Display Reverse",
                         RadioSettingValueList(
                             ["Standard", "Reverse"],
                             current_index=_settings.DispDir))
        normal.append(rs)

        # Enhance Function
        rs = RadioSetting("EnhanceFunc", "Enhance Function",
                         RadioSettingValueList(
                             ["OFF", "ON"],
                             current_index=_settings.EnhanceFunc))
        normal.append(rs)

        # TOT[s]
        tot_options = ["OFF", "15", "30", "45", "60", "75", "90", "105", "120", 
                      "135", "150", "165", "180", "195", "210"]
        idx = min(_settings.tot, len(tot_options) - 1) if _settings.tot >= 0 else 0
        rs = RadioSetting("tot", "TOT[s]",
                         RadioSettingValueList(tot_options, current_index=idx))
        normal.append(rs)

        # TOT Alerts
        tot_alerts_options = ["OFF"] + [str(x) for x in range(1, 11)]
        idx = min(_settings.pre_tot, len(tot_alerts_options) - 1) if _settings.pre_tot >= 0 else 0
        rs = RadioSetting("pre_tot", "TOT Alerts",
                         RadioSettingValueList(tot_alerts_options, current_index=idx))
        normal.append(rs)

        # Vox Enable
        rs = RadioSetting("voxsw", "Vox Enable",
                         RadioSettingValueBoolean(_settings.voxsw))
        normal.append(rs)

        # Vox Level
        rs = RadioSetting("vox_lv", "Vox Level",
                         RadioSettingValueInteger(1, 9, 
                                                max(1, min(9, _settings.vox_lv))))
        normal.append(rs)

        # Vox Delay Detect[s] - Values from 1.0 to 10.0 in 0.5 steps
        vox_delay_options = ["1.0", "1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0", 
                             "5.5", "6.0", "6.5", "7.0", "7.5", "8.0", "8.5", "9.0", "9.5", "10.0"]

        # Map stored value (10-100) to list index (0-18)
        stored_value = _settings.vox_det_time
        # Ensure value is within valid range
        if stored_value < 10:
            stored_value = 10
        elif stored_value > 100:
            stored_value = 100

        # Calculate index: (stored_value - 10) / 5
        # For example: 15 -> (15-10)/5 = 1 (index for "1.5")
        idx = (stored_value - 10) // 5

        rs = RadioSetting("vox_det_time", "Vox Delay Detect[s]",
                         RadioSettingValueList(vox_delay_options, current_index=idx))

        # Set apply callback to convert selected value back to stored format
        def apply_vox_delay(setting, obj):
            # Get the selected index and convert to stored value
            selected_idx = vox_delay_options.index(str(setting.value))
            # Store as integer = (index * 5) + 10
            # For example: index 1 ("1.5") -> (1*5)+10 = 15
            obj.vox_det_time = (selected_idx * 5) + 10

        rs.set_apply_callback(apply_vox_delay, _settings)
        normal.append(rs)

        # More Settings
        # Engineering Mode
        rs = RadioSetting("Engineering", "Engineering Mode",
                         RadioSettingValueBoolean(_settings.Engineering))
        more.append(rs)

        # Contact ID (gps_id)
        rs = RadioSetting("gps_id", "Contact ID",
                         RadioSettingValueInteger(0, 255, _settings.gps_id))
        more.append(rs)

        # 1750hz
        hz_options = ["1000Hz", "1450Hz", "1750Hz", "2100Hz"]
        idx = min(_settings.hz_to_1750, len(hz_options) - 1) if _settings.hz_to_1750 >= 0 else 0
        rs = RadioSetting("hz_to_1750", "1750hz",
                        RadioSettingValueList(hz_options, current_index=idx))
        more.append(rs)

        # NOAA
        rs = RadioSetting("NOAA", "NOAA",
                        RadioSettingValueList(
                            ["OFF", "ON"],
                            current_index=_settings.NOAA))
        more.append(rs)

        # NOAA Channel
        noaa_ch_options = [str(x) for x in range(1, 11)]  # "1" to "10"
        # Use min() to ensure value doesn't exceed list bounds and subtract 1 from length
        idx = min(_settings.noaa_ch, len(noaa_ch_options) - 1) if _settings.noaa_ch >= 0 else 0
        rs = RadioSetting("noaa_ch", "NOAA Channel",
                         RadioSettingValueList(noaa_ch_options, current_index=idx))
        more.append(rs)

        # WX (Weather)
        rs = RadioSetting("TianQi", "WX",
                        RadioSettingValueList(
                            ["OFF", "ON"],
                            current_index=_settings.TianQi))
        more.append(rs)

        # Falling Alarm (DaoDi)
        rs = RadioSetting("daodi", "Falling Alarm",
                        RadioSettingValueList(
                            ["OFF", "ON"],
                            current_index=_settings.daodi))
        more.append(rs)

        # PF key function options
        pf_key_options = [
            "None", "Scan On/Off", "Monitor", "Flashlight", "FM Radio", "Emergency", 
            "GPS", "Freq Measureing", "Bluetooth", "1750Hz", "Falling Alarm", 
            "One Touch Call", "Zone Change", "Battery Indicator", "TX Power", "VOX On/Off"
        ]

        # PF1 Short Press
        idx = min(_settings.skey1, len(pf_key_options) - 1) if _settings.skey1 >= 0 else 0
        rs = RadioSetting("skey1", "PF1 Short Press",
                        RadioSettingValueList(pf_key_options, current_index=idx))
        more.append(rs)

        # PF1 Long Press
        idx = min(_settings.lkey1, len(pf_key_options) - 1) if _settings.lkey1 >= 0 else 0
        rs = RadioSetting("lkey1", "PF1 Long Press",
                        RadioSettingValueList(pf_key_options, current_index=idx))
        more.append(rs)

        # PF2 Short Press
        idx = min(_settings.skey2, len(pf_key_options) - 1) if _settings.skey2 >= 0 else 0
        rs = RadioSetting("skey2", "PF2 Short Press",
                        RadioSettingValueList(pf_key_options, current_index=idx))
        more.append(rs)

        # PF2 Long Press
        idx = min(_settings.lkey2, len(pf_key_options) - 1) if _settings.lkey2 >= 0 else 0
        rs = RadioSetting("lkey2", "PF2 Long Press",
                        RadioSettingValueList(pf_key_options, current_index=idx))
        more.append(rs)

        # Alone Worker Enable
        rs = RadioSetting("lonework", "Alone Worker Enable",
                         RadioSettingValueBoolean(_settings.lonework))
        more.append(rs)

        # Lone Work Response
        rs = RadioSetting("lone_work_rsp", "Lone Work Response",
                         RadioSettingValueInteger(0, 255, _settings.lone_work_rsp))
        more.append(rs)

        # Lone Work Reminder
        rs = RadioSetting("lone_work_tim", "Lone Work Reminder",
                         RadioSettingValueInteger(0, 255, _settings.lone_work_tim))
        more.append(rs)

        # Bluetooth enable
        rs = RadioSetting("BlueT", "Bluetooth enable",
                         RadioSettingValueBoolean(_settings.BlueT))
        more.append(rs)

        # Bluetooth name
        bt_name = ""
        try:
            bt_name = bytes([int(x) for x in _settings.bluet_name]).decode('GB2312', 'ignore').rstrip('\0 ')
        except:
            bt_name = ""

        rs = RadioSetting("bluet_name", "Bluetooth Name",
                         RadioSettingValueString(0, 16, bt_name))

        def apply_bt_name(setting, obj):
            name = str(setting.value).ljust(16, '\0')
            try:
                obj.bluet_name = bytes(name[:16], 'GB2312')
            except:
                obj.bluet_name = bytes([0xFF] * 16)

        rs.set_apply_callback(apply_bt_name, _settings)
        more.append(rs)

        # GPS enable
        rs = RadioSetting("GpsSW", "GPS enable",
                         RadioSettingValueBoolean(_settings.GpsSW))
        more.append(rs)

        # APRS enable
        rs = RadioSetting("aprssw", "APRS enable",
                         RadioSettingValueBoolean(_settings.aprssw))
        more.append(rs)

        # GPS zone
        gps_zone_options = [str(x) for x in range(-12, 13)]  # "-12" to "12"
        # Value is stored 0-indexed, where 0 represents -12, 12 represents 0, 24 represents +12
        idx = min(_settings.gps_zone, len(gps_zone_options) - 1) if _settings.gps_zone >= 0 else 0
        rs = RadioSetting("gps_zone", "GPS zone",
                        RadioSettingValueList(gps_zone_options, current_index=idx))

        # Apply callback to convert between displayed value and stored value
        def apply_gps_zone(setting, obj):
            # Convert from display value to stored value (0-indexed)
            val = gps_zone_options.index(str(setting.value))  # Gets index 0-24
            obj.gps_zone = val  # Store directly

        rs.set_apply_callback(apply_gps_zone, _settings)
        more.append(rs)

        # Keep all existing settings for the other groups

        # Display settings - keep what was already defined
        # Re-add DispDir which was already moved to Normal Settings
        # Any other display settings would be added here

        # Audio settings
        # (Empty in original, can be populated if needed)

        # DTMF settings
        rs = RadioSetting("dtmf.DtmfSw", "DTMF Enable",
                         RadioSettingValueBoolean(_dtmf.DtmfSw))
        dtmf.append(rs)

        rs = RadioSetting("dtmf.CodeSpeed", "DTMF Code Speed",
                         RadioSettingValueInteger(0, 9, _dtmf.CodeSpeed))
        dtmf.append(rs)

        # Add settings for DTMF BOT/EOT codes
        for i, code_type in enumerate(["Bot", "Eot", "Stun", "Kill"]):
            field = getattr(_dtmf, code_type)
            code_str = ""
            for j in range(16):
                if int(field[j]) != 0xFF:
                    code_str += chr(int(field[j]))

            rs = RadioSetting(f"dtmf.{code_type}", f"DTMF {code_type} Code",
                             RadioSettingValueString(0, 16, code_str))

            def apply_dtmf_code(setting, obj, field_name):
                code = str(setting.value).strip()
                field = getattr(obj, field_name)
                for i in range(16):
                    if i < len(code):
                        field[i] = ord(code[i])
                    else:
                        field[i] = 0xFF

            rs.set_apply_callback(apply_dtmf_code, _dtmf, code_type)
            dtmf.append(rs)

        # Zone settings
        active_zones = self._calculate_active_zones()
        rs = RadioSetting("_zone_count_display", "Number of Zones (Auto-Calculated)",
                         RadioSettingValueInteger(0, ZONE_MAX_NUM, active_zones))
        rs.set_doc("Number of active zones (automatically calculated based on defined zone names and channel assignments)")
        zones_group.append(rs)

        # Add zone name settings
        for i in range(ZONE_MAX_NUM):
            zone = _zones.zone_info[i]
            # Get zone name
            zone_name = ""
            try:
                raw_name = bytes([int(x) for x in zone.zone_name])
                name_end = raw_name.find(b'\x00')
                if name_end != -1:
                    raw_name = raw_name[:name_end]
                zone_name = raw_name.decode('GB2312', errors='replace').rstrip()
            except Exception:
                zone_name = f"Zone {i+1}" if i < active_zones else ""

            rs = RadioSetting(f"zone_name_{i}", f"Zone {i+1} Name",
                             RadioSettingValueString(0, 16, zone_name))

            def apply_zone_name(setting, zones, idx):
                name = str(setting.value).ljust(16, '\0')
                try:
                    zones.zone_info[idx].zone_name = bytes(name[:16], 'GB2312')
                except Exception:
                    zones.zone_info[idx].zone_name = bytes(name[:16], 'ascii', 'replace')

            rs.set_apply_callback(apply_zone_name, _zones, i)
            zones_group.append(rs)

        # Scan settings
        # Scan Mode (Time/Carrier/Search)
        scan_modes = ["Time", "Carrier", "Search"]
        idx = min(_scan.scanmode, len(scan_modes) - 1) if _scan.scanmode >= 0 else 0
        rs = RadioSetting("scan.scanmode", "Scan Mode",
                         RadioSettingValueList(scan_modes, current_index=idx))
        rs.set_doc("Determines how scan pauses on active channels")
        scan.append(rs)

        # Background Scan Time
        # Background Scan Time (0.5s - 5.0s in 0.1s increments)
        backscan_options = [
            "0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0",
            "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0",
            "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0",
            "3.1", "3.2", "3.3", "3.4", "3.5", "3.6", "3.7", "3.8", "3.9", "4.0",
            "4.1", "4.2", "4.3", "4.4", "4.5", "4.6", "4.7", "4.8", "4.9", "5.0"
        ]
        # Convert from stored value (in microseconds) to list index
        # Stored value ranges from 500ms to 5000ms (0.5s to 5.0s)
        backscan_time_ms = int(_scan.backscantime)
        # Find closest match in our list (0.5s = 500ms = index 0)
        idx = min(max(int(backscan_time_ms - 1), 5), len(backscan_options) - 1)
        rs = RadioSetting("scan.backscantime", "Background Scan Time",
                         RadioSettingValueList(backscan_options, current_index=idx))
        rs.set_doc("Duration to monitor each channel during scan (seconds)")
        scan.append(rs)

        # RX Resume Time (0.1s - 5.0s in 0.1s increments)
        rx_resume_options = [
            "0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0",
            "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0",
            "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0",
            "3.1", "3.2", "3.3", "3.4", "3.5", "3.6", "3.7", "3.8", "3.9", "4.0",
            "4.1", "4.2", "4.3", "4.4", "4.5", "4.6", "4.7", "4.8", "4.9", "5.0"
        ]
        # Convert from stored value (typically 1-50 for 0.1s to 5.0s)
        rx_resume_time = int(_scan.rxresumetime)
        idx = min(max(rx_resume_time - 1, 0), len(rx_resume_options) - 1)
        rs = RadioSetting("scan.rxresumetime", "RX Resume Time",
                        RadioSettingValueList(rx_resume_options, current_index=idx))
        rs.set_doc("Time before scan resumes after receiving a signal (seconds)")
        scan.append(rs)

        # TX Resume Time (0.1s - 5.0s in 0.1s increments)
        tx_resume_options = [
            "0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0",
            "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0",
            "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0",
            "3.1", "3.2", "3.3", "3.4", "3.5", "3.6", "3.7", "3.8", "3.9", "4.0",
            "4.1", "4.2", "4.3", "4.4", "4.5", "4.6", "4.7", "4.8", "4.9", "5.0"
        ]
        # Convert from stored value (typically 1-50 for 0.1s to 5.0s)
        tx_resume_time = int(_scan.txresumetime)
        idx = min(max(tx_resume_time - 1, 0), len(tx_resume_options) - 1)
        rs = RadioSetting("scan.txresumetime", "TX Resume Time",
                        RadioSettingValueList(tx_resume_options, current_index=idx))
        rs.set_doc("Time before scan resumes after transmission (seconds)")
        scan.append(rs)

        # Priority Scan Toggle
        rs = RadioSetting("scan.priorityscan", "Priority Scan",
                        RadioSettingValueBoolean(_scan.priorityscan))
        rs.set_doc("Enable priority channel scanning")
        scan.append(rs)

        # Priority Channel (1-64)
        priority_ch_val = _scan.prioritychannel
        # Ensure the value is within the valid range of 1-64
        priority_ch_val = max(1, min(64, priority_ch_val)) if priority_ch_val > 0 else 1
        rs = RadioSetting("scan.prioritychannel", "Priority Channel",
                        RadioSettingValueInteger(1, 64, priority_ch_val))
        rs.set_doc("Channel to prioritize during scan (1-64)")
        scan.append(rs)

        # Scan Range
        scan_range_options = ["All", "Memory Scan"]
        idx = min(_scan.scanrange, len(scan_range_options) - 1) if _scan.scanrange >= 0 else 0
        rs = RadioSetting("scan.scanrange", "Scan Range",
                        RadioSettingValueList(scan_range_options, current_index=idx))
        rs.set_doc("Range of channels to include in scan")
        scan.append(rs)

        # Return Channel Type
        return_channel_options = [
            "Selected", "Selected + Current", "Last Received Call", 
            "Last Used", "Priority Channel", "Priority Channel + Current"
        ]
        idx = min(_scan.returnchanneltype, len(return_channel_options) - 1) if _scan.returnchanneltype >= 0 else 0
        rs = RadioSetting("scan.returnchanneltype", "Return Channel Type",
                        RadioSettingValueList(return_channel_options, current_index=idx))
        rs.set_doc("Which channel to return to when scan stops")
        scan.append(rs)

        # Bluetooth settings
        # Bluetooth PIN
        bt_password = ""
        try:
            bt_password = bytes([int(x) for x in _settings.bt_password]).decode('ascii', 'ignore').rstrip('\0 ')
        except:
            bt_password = ""

        rs = RadioSetting("bt_password", "Bluetooth PIN",
                         RadioSettingValueString(0, 4, bt_password))

        def apply_bt_password(setting, obj):
            password = str(setting.value).ljust(4, '\0')
            obj.bt_password = bytes(password[:4], 'ascii')

        rs.set_apply_callback(apply_bt_password, _settings)
        bluetooth.append(rs)

        # Bluetooth hold time
        rs = RadioSetting("bt_hold", "Bluetooth Hold Time",
                         RadioSettingValueInteger(0, 60, _settings.bt_hold))
        bluetooth.append(rs)

        # Bluetooth RX delay
        rs = RadioSetting("bt_rxdly", "Bluetooth RX Delay",
                         RadioSettingValueInteger(0, 60, _settings.bt_rxdly))
        bluetooth.append(rs)

        # Bluetooth Mic
        rs = RadioSetting("bt_mic", "Bluetooth Mic",
                          RadioSettingValueInteger(0, 255, _settings.bt_mic))
        bluetooth.append(rs)

        # Bluetooth Speaker
        rs = RadioSetting("bt_spk", "Bluetooth Speaker",
                          RadioSettingValueInteger(0, 255, _settings.bt_spk))
        bluetooth.append(rs)

        return group

    def _calculate_active_zones(self):
        """Calculate how many zones have valid names or channels"""
        active_zones = 0
        for i in range(ZONE_MAX_NUM):
            zone = self._memobj.zones.zone_info[i]
            try:
                raw_name = bytes([int(x) for x in zone.zone_name])
                name_end = raw_name.find(b'\x00')
                if name_end != -1:
                    raw_name = raw_name[:name_end]
                name = raw_name.decode('GB2312', errors='replace').strip()

                # If zone has channels or a custom name, count it as active
                if zone.chn_num > 0 or (name and name != f"Zone {i+1}"):
                    active_zones = max(active_zones, i + 1)
            except Exception:
                # Skip zones with encoding issues
                pass

        # Ensure at least one zone is always active
        return max(1, active_zones)

    def set_settings(self, settings):
        """Set radio settings"""
        _settings = self._memobj.settings
        _dtmf = self._memobj.DtmfSysInfo
        _zones = self._memobj.zones
        _scan = self._memobj.scandata
        _vfos = self._memobj.vfo  # Access VFO structures

        # Process each setting group
        for element in settings:
            if not isinstance(element, RadioSetting):
                # This is a RadioSettingGroup, process its members recursively
                self.set_settings(element)
                continue

            # Skip read-only elements or those with apply callbacks
            if element.has_apply_callback():
                element.run_apply_callback()
                continue

            # Skip our read-only zone count display
            if element.get_name() == "_zone_count_display":
                continue

            # Extract object and setting names
            objname, setting = self._extract_setting_name(element.get_name())

            # Handle VFO settings
            if objname.startswith("vfo"):
                vfo_idx = int(objname[3:])
                vfo = _vfos[vfo_idx]

                if setting == "rx_freq":
                    # Convert MHz to Hz and encode as BCD
                    freq_mhz = float(element.value)
                    freq_hz = int(freq_mhz * 1000000)
                    vfo.rx_freq = self._bcd_encode_freq(freq_hz)

                elif setting == "tx_freq":
                    # Convert MHz to Hz and encode as BCD
                    freq_mhz = float(element.value)
                    freq_hz = int(freq_mhz * 1000000)
                    vfo.tx_freq = self._bcd_encode_freq(freq_hz)

                elif setting == "offsetdir":
                    # 0=None, 1=+, 2=-
                    options = ["None", "+", "-"]
                    idx = options.index(str(element.value))
                    vfo.offsetdir = idx

                elif setting == "freq_diff":
                    # This is handled by rx_freq, tx_freq and offsetdir
                    continue

                elif setting == "freq_step":
                    # Convert step from string to index
                    steps = ["2.5K", "5.0K", "6.25K", "8.33K", "10.0K", "12.5K", 
                            "20.0K", "25.0K", "30.0K", "50.0K", "100.0K"]
                    idx = steps.index(str(element.value))
                    vfo.freq_step = idx

                elif setting == "rx_tone":
                    # Handle CTCSS/DCS decode tone
                    tone_val = str(element.value)
                    if tone_val == "None":
                        vfo.rx_ctcss_dcs_h = 0
                        vfo.rx_ctcss_dcs_l = 0
                    elif tone_val.endswith("Hz"):
                        # CTCSS tone
                        tone = float(tone_val.replace("Hz", ""))
                        h, l = self._encode_tone("Tone", tone)
                        vfo.rx_ctcss_dcs_h = h
                        vfo.rx_ctcss_dcs_l = l
                    elif tone_val.startswith("D"):
                        # DCS code - extract code and polarity
                        code = int(tone_val[1:-1])
                        pol = "I" if tone_val.endswith("I") else "N"
                        h, l = self._encode_tone("DTCS", code, pol)
                        vfo.rx_ctcss_dcs_h = h
                        vfo.rx_ctcss_dcs_l = l

                elif setting == "tx_tone":
                    # Handle CTCSS/DCS encode tone
                    tone_val = str(element.value)
                    if tone_val == "None":
                        vfo.tx_ctcss_dcs_h = 0
                        vfo.tx_ctcss_dcs_l = 0
                    elif tone_val.endswith("Hz"):
                        # CTCSS tone
                        tone = float(tone_val.replace("Hz", ""))
                        h, l = self._encode_tone("Tone", tone)
                        vfo.tx_ctcss_dcs_h = h
                        vfo.tx_ctcss_dcs_l = l
                    elif tone_val.startswith("D"):
                        # DCS code - extract code and polarity
                        code = int(tone_val[1:-1])
                        pol = "I" if tone_val.endswith("I") else "N"
                        h, l = self._encode_tone("DTCS", code, pol)
                        vfo.tx_ctcss_dcs_h = h
                        vfo.tx_ctcss_dcs_l = l

                elif setting == "wideth":
                    # 0=12.5kHz, 1=25kHz
                    vfo.wideth = 1 if str(element.value) == "25kHz" else 0

                elif setting == "power":
                    # 0=Low, 1=Mid, 2=High
                    power_options = ["Low", "Mid", "High"]
                    vfo.power = power_options.index(str(element.value))

                elif setting == "signaltype":
                    # Signal type options
                    signal_options = ["None", "DTMF", "2Tone", "5Tone", "MDC", "BIS", "APRS"]
                    vfo.signaltype = signal_options.index(str(element.value))

                elif setting in ("dtmf_idx", "twotone_idx", "fivetone_idx", "mdc_idx"):
                    # Direct integer assignments
                    setattr(vfo, setting, int(element.value))

                elif setting == "dtmfptt" or setting == "fivetoneptt":
                    # 0=OFF, 1=BOT, 2=EOT, 3=BOTH
                    ptt_options = ["OFF", "BOT", "EOT", "BOTH"]
                    setattr(vfo, setting, ptt_options.index(str(element.value)))

                elif setting == "talkaround":
                    # ON/OFF boolean
                    vfo.talkaround = 1 if str(element.value) == "ON" else 0

                elif setting == "sqtype":
                    # Squelch type options
                    sq_options = ["None", "CTDCS", "Optional", "CTDCS and Optional"]
                    vfo.sqtype = sq_options.index(str(element.value))

                elif setting == "emerglist":
                    # Emergency system options
                    emerg_options = ["None"] + [str(i) for i in range(1, 11)]
                    vfo.emerglist = emerg_options.index(str(element.value))

                elif setting == "txdis":
                    # Transmit disable (Launch Banned)
                    vfo.txdis = 1 if str(element.value) == "ON" else 0

                elif setting == "jumpfreq":
                    # Jump frequency options
                    jumpfreq_options = ["OFF", "ON"]
                    vfo.jumpfreq = jumpfreq_options.index(str(element.value))

                elif setting == "freqinvert":
                    # Frequency inversion
                    vfo.freqinvert = 1 if str(element.value) == "ON" else 0

            elif objname == "dtmf":
                obj = _dtmf

            elif objname == "scan":
                obj = _scan

            elif objname.startswith("zone_"):
                # Zone names are handled by apply_callback
                continue

            else:
                obj = _settings

            # Skip if we couldn't determine the object
            if obj is None:
                LOG.warning(f"Unknown object for setting {element.get_name()}")
                continue

            # Handle special cases for various settings
            if setting == "ch_a_zone" or setting == "ch_b_zone":
                # These are displayed 1-indexed but stored 0-indexed
                setattr(obj, setting, int(element.value) - 1)

            elif setting == "po_save_dly":
                # Map from display option to stored value (5, 10, 15, 20, 25)
                value_map = {"5": 5, "10": 10, "15": 15, "20": 20, "25": 25}
                setattr(obj, setting, value_map[str(element.value)])

            elif setting == "apo":
                # Map from display option to stored value
                value_map = {"OFF": 0, "30": 30, "60": 60, "120": 120, "240": 240, "480": 480}
                setattr(obj, setting, value_map[str(element.value)])

            elif setting == "tone":
                # Tone level is displayed as 1-5 but stored as 0-4
                if isinstance(element.value, int):
                    setattr(obj, setting, int(element.value) - 1)

            elif setting == "main_band":
                # A/B is stored as 0/1
                setattr(obj, setting, 0 if str(element.value) == "A" else 1)

            elif setting in ("ch_a_mode", "ch_b_mode"):
                # Convert VFO/MR to 0/1
                mode_map = {"VFO": 0, "MR": 1}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting in ("dispa_mode", "dispb_mode"):
                # Convert display mode to index
                mode_map = {"Frequency": 0, "Name": 1, "Number": 2, "Frequency + Name": 3}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "dual_mode":
                # Convert dual mode to index
                mode_map = {"Off": 0, "Dual Waiting": 1, "Single Waiting": 2}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "PownFace":
                # Convert power on screen to index
                mode_map = {"Picture": 0, "Character": 1, "Voltage": 2}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "blight_time":
                # Convert backlight time to index
                mode_map = {"Always": 0, "5 Sec": 1, "10 Sec": 2, "15 Sec": 3, "20 Sec": 4, "25 Sec": 5, "30 Sec": 6}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "voice":
                # Convert voice to index
                mode_map = {"Off": 0, "Chinese": 1, "English": 2}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "tone_switch":
                # Convert tone switch to ON/OFF as 0/1
                mode_map = {"OFF": 0, "ON": 1}
                setattr(obj, "tone", mode_map[str(element.value)])

            elif setting == "endtone":
                # Convert end tone to index
                mode_map = {"OFF": 0, "Mode 1": 1, "Mode 2": 2, "Mode 3": 3}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "LangSel":
                # Convert language to index
                mode_map = {"English": 0, "Chinese": 1}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "TailFreq":
                # Convert tail frequency to index
                mode_map = {"OFF": 0, "55hz": 1, "120°": 2, "180°": 3, "240°": 4}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "DispDir":
                # Convert display direction to index
                mode_map = {"Standard": 0, "Reverse": 1}
                setattr(obj, setting, mode_map[str(element.value)])

            elif setting == "hz_to_1750":
                # Map from display option to stored value
                value_map = {"1000Hz": 0, "1450Hz": 1, "1750Hz": 2, "2100Hz": 3}
                setattr(obj, setting, value_map[str(element.value)])

            elif setting == "noaa_ch" and str(element.value).isdigit():
                # NOAA channels are displayed as 1-10 but stored as 0-9
                setattr(obj, setting, int(element.value) - 1)

            elif setting == "vox_det_time":
                # vox_det_time is already handled by apply_callback
                continue

            elif setting in ("NOAA", "TianQi", "daodi", "autokey", "EnhanceFunc"):
                # Convert ON/OFF to boolean values
                setattr(obj, setting, 1 if str(element.value) == "ON" else 0)

            elif setting in ("skey1", "skey2", "lkey1", "lkey2"):
                # Map PF key functions to index values
                pf_functions = [
                    "None", "Scan On/Off", "Monitor", "Flashlight", "FM Radio", "Emergency", 
                    "GPS", "Freq Measureing", "Bluetooth", "1750Hz", "Falling Alarm", 
                    "One Touch Call", "Zone Change", "Battery Indicator", "TX Power", "VOX On/Off"
                ]
                try:
                    value = pf_functions.index(str(element.value))
                    setattr(obj, setting, value)
                except ValueError:
                    LOG.warning(f"Unknown PF function: {element.value}")

            elif setting == "scanmode":
                # Map scan mode to index
                scan_modes = ["Time", "Carrier", "Search"]
                try:
                    value = scan_modes.index(str(element.value))
                    setattr(obj, setting, value)
                except ValueError:
                    LOG.warning(f"Unknown scan mode: {element.value}")
            #####
            elif setting == "po_save":
                # Convert power save options to numeric values
                power_save_map = {"OFF": 0, "1:1": 1, "1:2": 2, "1:4": 3}
                setattr(obj, setting, power_save_map[str(element.value)])
                
            elif setting == "busylock":
                # Convert busy lock options to numeric values
                busylock_map = {"OFF": 0, "Carrier": 1, "QT/DQT": 2}
                setattr(obj, setting, busylock_map[str(element.value)])
                
            elif setting == "tot":
                # Convert timeout options to numeric values
                if str(element.value) == "OFF":
                    setattr(obj, setting, 0)
                else:
                    # For numeric values (15, 30, etc.), store the index
                    tot_options = ["OFF", "15", "30", "45", "60", "75", "90", "105", 
                                  "120", "135", "150", "165", "180", "195", "210"]
                    try:
                        idx = tot_options.index(str(element.value))
                        setattr(obj, setting, idx)
                    except ValueError:
                        LOG.warning(f"Unknown TOT value: {element.value}")
                
            elif setting == "pre_tot":
                # Convert TOT alerts options to numeric values
                if str(element.value) == "OFF":
                    setattr(obj, setting, 0)
                else:
                    # For numeric values (1-10), store the number directly
                    try:
                        value = int(element.value)
                        setattr(obj, setting, value)
                    except ValueError:
                        LOG.warning(f"Invalid pre_tot value: {element.value}")

            elif objname == "scan" and setting == "backscantime":
                # Convert display value (0.5-5.0 seconds) to stored value (in milliseconds)
                try:
                    # Convert string like "0.1" to index 1
                    value_seconds = float(str(element.value))
                    # Store value as index (1-50)
                    stored_value = int(value_seconds * 10)
                    setattr(obj, setting, stored_value)
                except ValueError:
                    LOG.warning(f"Invalid backscan time value: {element.value}")

            elif objname == "scan" and setting in ("rxresumetime", "txresumetime"):
                # Convert resume times from string (0.1-5.0 seconds) to numeric index (1-50)
                try:
                    # Convert string like "0.1" to index 1
                    value_seconds = float(str(element.value))
                    # Store value as index (1-50)
                    stored_value = int(value_seconds * 10)
                    setattr(obj, setting, stored_value)
                except ValueError:
                    LOG.warning(f"Invalid resume time value: {element.value}")

            elif objname == "scan" and setting == "returnchanneltype":
                # Convert return channel type from string to index
                return_options = [
                    "Selected", "Selected + Current", "Last Received Call", 
                    "Last Used", "Priority Channel", "Priority Channel + Current"
                ]
                try:
                    idx = return_options.index(str(element.value))
                    setattr(obj, setting, idx)
                except ValueError:
                    LOG.warning(f"Unknown return channel type: {element.value}")

            elif objname == "scan" and setting == "prioritychannel":
                # Store priority channel directly (valid range is 1-64)
                value = int(element.value)
                if 1 <= value <= 64:
                    setattr(obj, setting, value)
                else:
                    LOG.warning(f"Invalid priority channel value: {value}")
                    setattr(obj, setting, 1)  # Default to first channel if invalid

            elif objname == "scan" and setting == "scanrange":
                # Convert scan range from string to index
                range_options = ["All", "Memory Scan"]
                try:
                    idx = range_options.index(str(element.value))
                    setattr(obj, setting, idx)
                except ValueError:
                    LOG.warning(f"Unknown scan range: {element.value}")

            else:
                # Try to set the value directly
                try:
                    value = element.value
                    if isinstance(value, RadioSettingValueBoolean):
                        value = bool(value)
                    elif isinstance(value, RadioSettingValueInteger):
                        value = int(value)
                    elif isinstance(value, RadioSettingValueString):
                        value = str(value)
                    elif isinstance(value, RadioSettingValueList):
                        # For list values that aren't handled above,
                        # try to convert to int only if it's a digit string
                        value = str(value)
                        if value.isdigit():
                            value = int(value)
                        # Otherwise keep as string

                    setattr(obj, setting, value)
                except Exception as e:
                    LOG.warning(f"Error setting {setting}: {e}")

        # After all settings are processed, perform updates

        # Update zones in memory map
        self._update_zones_in_memory()

        # Update zone_total based on active zones
        _zones.zone_total = self._calculate_active_zones()

        # Ensure channels in zones are ordered correctly
        self.ensure_correct_zone_ordering()


    def _extract_setting_name(self, name):
        """Split setting name into object and attribute parts"""
        if "." in name:
            objname, setting = name.split(".", 1)
        else:
            objname = ""
            setting = name
        return objname, setting

    def _update_zones_in_memory(self):
        """Update zones structure to ensure consistency"""
        # Ensure zone_total is valid
        if self._memobj.zones.zone_total > ZONE_MAX_NUM:
            self._memobj.zones.zone_total = ZONE_MAX_NUM

        # Check each zone
        for i in range(self._memobj.zones.zone_total):
            zone = self._memobj.zones.zone_info[i]

            # Ensure zone has valid channel count
            if zone.chn_num > ZONE_MAX_CHN_NUM:
                zone.chn_num = ZONE_MAX_CHN_NUM

            # Check if all channels in the zone are valid
            j = 0
            while j < zone.chn_num:
                channel_num = zone.chn_id[j]
                if channel_num < 1 or channel_num > MAX_CHN_NUM:
                    # Remove invalid channel
                    for k in range(j, zone.chn_num - 1):
                        zone.chn_id[k] = zone.chn_id[k + 1]
                    zone.chn_num -= 1
                else:
                    j += 1

    def get_zone_configuration(self):
        """Return the zone configuration as (number_of_zones, channels_per_zone)"""
        return (ZONE_MAX_NUM, ZONE_MAX_CHN_NUM)

    def ensure_correct_zone_ordering(self):
        """Ensure all channels in all zones are in their correct positions"""
        # Process each zone
        for zone_idx in range(self._memobj.zones.zone_total):
            zone = self._memobj.zones.zone_info[zone_idx]
            
            if zone.chn_num == 0:
                continue
            
            # First, collect all valid channels
            valid_channels = []
            invalid_positions = []
            for i in range(zone.chn_num):
                channel_num = zone.chn_id[i] + 1  # Convert to 1-based
                if self._is_channel_valid(channel_num):
                    valid_channels.append(channel_num)
                else:
                    invalid_positions.append(i)

            # Remove invalid channels (working backwards to avoid index problems)
            for pos in sorted(invalid_positions, reverse=True):
                for j in range(pos, zone.chn_num - 1):
                    zone.chn_id[j] = zone.chn_id[j + 1]
                # Set the last position to 0xFFFF
                zone.chn_id[zone.chn_num - 1] = 0xFFFF
                zone.chn_num -= 1

            # Now rebuild the zone with channels in correct positions
            zone.chn_num = 0
            for channel_num in sorted(valid_channels):
                # Calculate correct zone and position
                correct_zone = (channel_num - 1) // ZONE_MAX_CHN_NUM
                position_in_zone = (channel_num - 1) % ZONE_MAX_CHN_NUM

                # If this channel belongs in this zone
                if correct_zone == zone_idx:
                    # If we need to expand the zone to accommodate this position
                    if position_in_zone >= zone.chn_num:
                        # Fill gaps with 0xFFFF
                        for i in range(zone.chn_num, position_in_zone):
                            zone.chn_id[i] = 0xFFFF
                        zone.chn_id[position_in_zone] = channel_num - 1  # Store 0-based
                        zone.chn_num = position_in_zone + 1
                    else:
                        # Insert at correct position, shifting others
                        for i in range(zone.chn_num, position_in_zone, -1):
                            zone.chn_id[i] = zone.chn_id[i - 1]
                        zone.chn_id[position_in_zone] = channel_num - 1
                        zone.chn_num += 1

    def add_channel_to_zone(self, zone_index, channel_number):
        """Add a channel to a specified zone"""
        if zone_index >= ZONE_MAX_NUM:
            return False

        # Verify channel is valid
        if not self._is_channel_valid(channel_number):
            return False

        # Get zone information
        zone = self._memobj.zones.zone_info[zone_index]

        # Check if the zone is at capacity
        if zone.chn_num >= ZONE_MAX_CHN_NUM:
            return False

        # Check if channel already exists in this zone
        for i in range(zone.chn_num):
            if zone.chn_id[i] == channel_number-1:
                return True

        # Calculate the proper position for this channel within the zone
        expected_position = (channel_number - 1) % ZONE_MAX_CHN_NUM

        # Check if this channel belongs in this zone per the formula
        expected_zone = (channel_number - 1) // ZONE_MAX_CHN_NUM
        
        # If the channel's proper position is beyond the current zone size,
        # fill in empty slots
        if expected_position >= zone.chn_num:
            # Fill any gaps with 0xFFFF (invalid channel marker)
            for i in range(zone.chn_num, expected_position):
                zone.chn_id[i] = 0xFFFF
            zone.chn_id[expected_position] = channel_number-1
            zone.chn_num = expected_position + 1
        else:
            # Insert at the right position, shifting others if needed
            for i in range(zone.chn_num, expected_position, -1):
                zone.chn_id[i] = zone.chn_id[i-1]
            # Place the channel at its correct position
            zone.chn_id[expected_position] = channel_number-1
            zone.chn_num += 1

        # Update the zone total count if needed
        if zone_index + 1 > self._memobj.zones.zone_total:
            self._memobj.zones.zone_total = zone_index + 1

        return True

    def remove_channel_from_zone(self, zone_index, channel_number):
        """Remove a channel from a specified zone"""
        if zone_index >= ZONE_MAX_NUM:
            return False
    
        # Get zone information
        zone = self._memobj.zones.zone_info[zone_index]
    
        # Find the channel in this zone
        found = False
        for i in range(zone.chn_num):
            if zone.chn_id[i] == channel_number-1:
                found = True
                # Shift all channels after this one up by one position
                for j in range(i, zone.chn_num - 1):
                    zone.chn_id[j] = zone.chn_id[j + 1]
                # Set the now-unused last position to 0xFFFF (invalid marker)
                zone.chn_id[zone.chn_num - 1] = 0xFFFF
                zone.chn_num -= 1
                break
    
        return found

    @classmethod
    def match_model(cls, filedata, filename):
        """Match the opened/downloaded image to the correct version"""
        # This radio has a fixed memory size
        return len(filedata) == DP_DATA_LEN