#!/usr/bin/env python3

#This is free and unencumbered software released into the public domain.
#
#Anyone is free to copy, modify, publish, use, compile, sell, or
#distribute this software, either in source code form or as a compiled
#binary, for any purpose, commercial or non-commercial, and by any
#means.
#
#In jurisdictions that recognize copyright laws, the author or authors
#of this software dedicate any and all copyright interest in the
#software to the public domain. We make this dedication for the benefit
#of the public at large and to the detriment of our heirs and
#successors. We intend this dedication to be an overt act of
#relinquishment in perpetuity of all present and future rights to this
#software under copyright law.
#
#THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
#EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
#MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
#IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
#OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
#ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
#OTHER DEALINGS IN THE SOFTWARE.
#
#For more information, please refer to <http://unlicense.org/>

import serial
import time
import struct
import sys
import os
import logging
from contextlib import contextmanager

# --- Protocol Constants (unchanged) ---
CMD_INFO = b"INFORMATION"
CMD_END = b"END\x00"
CMD_DOWNLOAD = b"DOWNLOAD"
CMD_UPDATE = b"#UPDATE?"
CMD_NEW_HARDWARE = b"V2_00_00"
CMD_PRG = b"PROGRAM1"
CMD_ACK = b"ACK"
CMD_NACK = b"NACK"

CMD_FLASH = [
    bytes([70, 45, 80, 82, 79, 71, 255, 255]),   # F-PROG
    bytes([70, 45, 69, 82, 65, 83, 69, 255]),    # F-ERASE
    bytes([70, 45, 67, 79, 255, 255, 255, 255]), # F-CO
    bytes([70, 45, 77, 79, 68, 255, 255, 255]),  # F-MOD
    bytes([70, 45, 86, 69, 82, 255, 255, 255]),  # F-VER
    bytes([70, 45, 83, 78, 255, 255, 255, 255]), # F-SN
    bytes([70, 45, 84, 73, 77, 69, 255, 255]),   # F-TIME
]

# Constants
EXPECTED_FLASH_SIZE = 524288
MAX_READ_ATTEMPTS = 3
BLOCK_SIZE = 1024  # Standard block size for writing

HEADER_SIZE = 0x50  # 80 bytes
HEADER_SIGNATURE_1 = b'BaoFeng'
HEADER_SIGNATURE_2 = b'BF_5RH'
HEADER_VERSION_1 = b'V1.0.0.0'
HEADER_VERSION_2 = b'V2.0.0.0'
HEADER_VERSION_PREFIX = b'V'

# --- Setup Logging ---
def setup_logging(verbose=False, log_file='fw_update.log'):
    """Configure logging with appropriate level"""
    level = logging.DEBUG if verbose else logging.INFO
    logging.basicConfig(
        level=level,
        format='%(asctime)s - %(levelname)s - %(message)s',
        handlers=[
            logging.StreamHandler(),
            logging.FileHandler(log_file, mode='w')  # Overwrite previous log
        ]
    )
    return logging.getLogger('fw_updater')

# --- Helper Functions ---
@contextmanager
def safe_serial(port, baudrate, **kwargs):
    """Context manager for safer serial port handling"""
    ser = None
    try:
        ser = serial.Serial(port, baudrate, **kwargs)
        yield ser
    except serial.SerialException as e:
        raise RuntimeError(f"Serial port error: {e}")
    finally:
        if ser and ser.is_open:
            try:
                ser.close()
                logging.debug("Serial port closed")
            except Exception as e:
                logging.warning(f"Error closing serial port: {e}")

def read_exact(ser, length, timeout=5, description="data"):
    """Read exactly 'length' bytes from serial with improved error handling"""
    buf = b''
    start = time.time()
    attempts = 0
    
    while len(buf) < length:
        if ser.in_waiting:
            new_data = ser.read(min(ser.in_waiting, length - len(buf)))
            if new_data:
                buf += new_data
                attempts = 0  # Reset attempt counter on successful read
            else:
                attempts += 1
                if attempts >= MAX_READ_ATTEMPTS:
                    raise TimeoutError(f"Failed to read {description} after multiple attempts")
        
        elapsed = time.time() - start
        if elapsed > timeout:
            if buf:
                logging.error(f"Timeout reading {description}: Got {len(buf)}/{length} bytes")
            raise TimeoutError(f"Timeout waiting for {description} ({len(buf)}/{length} bytes received)")
        
        # Adaptive sleep - shorter when data is available or almost complete
        remaining_pct = (length - len(buf)) / length
        time.sleep(min(0.01, remaining_pct * 0.05))
    
    return buf

def write_and_wait_ack(ser, data, ack=CMD_ACK, timeout=5, description="command", retry_count=1):
    """Send data and wait for acknowledgement with improved error handling"""
    for attempt in range(retry_count + 1):
        try:
            ser.write(data)
            logging.debug(f"Sent {len(data)} bytes for {description}")
            resp = read_exact(ser, len(ack), timeout, f"ACK for {description}")
            if resp != ack:
                logging.warning(f"Attempt {attempt+1}/{retry_count+1}: Expected {ack!r}, got {resp!r}")
                if attempt < retry_count:
                    time.sleep(0.5)  # Wait before retry
                    continue
                logging.error(f"Failed to get proper ACK after {retry_count+1} attempts")
                return False
            return True
        except Exception as e:
            if attempt < retry_count:
                logging.warning(f"Attempt {attempt+1}/{retry_count+1} failed: {e}, retrying...")
                time.sleep(0.5)  # Wait before retry
            else:
                logging.error(f"Error in write_and_wait_ack for {description}: {e}")
                return False
    return False

def print_progress(percent, msg="", last_percent=[0]):
    """Display progress with ETA estimation"""
    # Only update if percentage changed significantly
    if percent - last_percent[0] >= 1 or percent >= 100:
        last_percent[0] = percent
        elapsed = time.time() - print_progress.start_time
        
        if percent > 0:
            eta_seconds = (elapsed / percent) * (100 - percent)
            eta = f"ETA: {int(eta_seconds/60)}m {int(eta_seconds%60)}s" if percent < 100 else "Complete"
            print(f"\r{percent:.1f}% {msg} [{eta}]", end='', flush=True)
        else:
            print(f"\r{percent:.1f}% {msg}", end='', flush=True)

# Initialize progress tracker
print_progress.start_time = 0

def verify_serial_port(port):
    """Check if the serial port is likely to be a programming adapter"""
    try:
        # List serial ports with descriptions
        import serial.tools.list_ports
        ports = list(serial.tools.list_ports.comports())
        
        # Find our port in the list
        port_info = next((p for p in ports if p.device == port), None)
        if not port_info:
            logging.warning(f"Port {port} not found in system device list")
            return False
            
        # Check for known programming adapter keywords
        adapter_keywords = ['CH340', 'CP210', 'FTDI', 'USB Serial', 'USB-Serial']
        if not any(keyword in port_info.description for keyword in adapter_keywords):
            logging.warning(f"Port {port} ({port_info.description}) might not be a programming adapter")
            if input("This doesn't appear to be a programming adapter. Continue? (y/n): ").lower() != 'y':
                return False
        
        return True
    except Exception as e:
        logging.warning(f"Failed to verify serial port: {e}")
        return True  # Continue anyway on error

def validate_firmware_header(header):
    """Validate the firmware header structure with exact specification"""
    if len(header) < HEADER_SIZE:
        return False, f"Header too short: {len(header)} bytes (expected {HEADER_SIZE})"
    
    # Check for BaoFeng signature (offset 0)
    if not header.startswith(HEADER_SIGNATURE_1):
        # Allow for variations in capitalization
        if not header.upper().startswith(HEADER_SIGNATURE_1.upper()):
            return False, f"Invalid header: Missing '{HEADER_SIGNATURE_1.decode()}' signature"
    
    # Check for BF_5RH signature (offset 16)
    if header[16:16+len(HEADER_SIGNATURE_2)] != HEADER_SIGNATURE_2:
        # Try with more flexibility (allow for FF padding between characters)
        sig_bytes = HEADER_SIGNATURE_2.replace(b'_', b'')
        matches = all(header[16+i] == sig_bytes[i] or header[16+i] == 0xFF for i in range(len(sig_bytes)))
        if not matches:
            return False, f"Invalid header: Missing '{HEADER_SIGNATURE_2.decode()}' signature"
    
    # Check for version string (offset 32)
    if header[32:32+1] != HEADER_VERSION_PREFIX:
        return False, "Invalid header: Missing version information"
    
    # Extract file size (offset 0x40, 32-bit big-endian)
    try:
        file_size = (header[0x40] << 24) | (header[0x41] << 16) | (header[0x42] << 8) | header[0x43]
        if file_size <= 0 or file_size > EXPECTED_FLASH_SIZE:
            return False, f"Invalid file size in header: {file_size} bytes"
        logging.info(f"Header indicates firmware size: {file_size} bytes")
    except IndexError:
        return False, "Header too short to extract file size"
        
    return True, "Header validation passed"

def validate_firmware_file(filepath, force=False):
    """Validate the firmware file before flashing"""
    if not os.path.exists(filepath):
        raise FileNotFoundError(f"Firmware file not found: {filepath}")
    
    size = os.path.getsize(filepath)
    if size == 0:
        raise ValueError("Firmware file is empty")
    
    # More comprehensive header check
    with open(filepath, "rb") as f:
        header = f.read(HEADER_SIZE)  # Read the full header
        
        valid, message = validate_firmware_header(header)
        if not valid:
            logging.warning(f"Firmware validation warning: {message}")
            if not force:
                print("\nWARNING: The firmware file may not be compatible with your radio.")
                print(f"Reason: {message}")
                print("This could potentially brick your device if continued.")
                confirm = input("Continue anyway? (y/n): ").lower()
                if confirm != 'y':
                    raise ValueError("Firmware validation failed, update aborted by user")
            logging.warning("Proceeding with firmware update despite validation warnings")
        else:
            logging.info("Firmware header validation passed")
    
    # Check file size is reasonable
    if size < HEADER_SIZE + 1024:
        logging.warning(f"Firmware file suspiciously small: {size} bytes")
        if not force:
            confirm = input("WARNING: Firmware file is unusually small. Continue? (y/n): ").lower()
            if confirm != 'y':
                raise ValueError("Firmware size validation failed, update aborted by user")
    
    logging.info(f"Firmware file size validation passed: {filepath} ({size} bytes)")
    return True

def hex_dump(data, start_addr=0, bytes_per_line=16):
    """Create a hexdump of binary data for logging purposes"""
    result = []
    for i in range(0, len(data), bytes_per_line):
        chunk = data[i:i+bytes_per_line]
        hex_values = ' '.join(f'{b:02X}' for b in chunk)
        ascii_values = ''.join(chr(b) if 32 <= b <= 126 else '.' for b in chunk)
        result.append(f"{start_addr+i:08X}: {hex_values.ljust(bytes_per_line*3)}  {ascii_values}")
    return '\n'.join(result)

# --- Main Updater Logic ---
def updater(port, baudrate, flash_path, verbose=False, force=False):
    """Update radio firmware with improved error handling and reporting"""
    logger = setup_logging(verbose)
    logger.info(f"Starting firmware update from {flash_path} on {port}")
    
    # Verify serial port
    if not verify_serial_port(port):
        logger.error(f"Serial port verification failed for {port}")
        return False
    
    # Validate firmware file
    try:
        validate_firmware_file(flash_path, force)
    except Exception as e:
        logger.error(f"Firmware validation failed: {e}")
        return False
    
    # Load FLASH data with proper size validation
    try:
        with open(flash_path, "rb") as f:
            FLASH = bytearray(f.read())
    except Exception as e:
        logger.error(f"Failed to read firmware file: {e}")
        return False
    
    # Print first 128 bytes for debugging
    if verbose:
        logger.debug("Firmware header dump:")
        logger.debug(hex_dump(FLASH[:128]))
    
    original_size = len(FLASH)
    if len(FLASH) < EXPECTED_FLASH_SIZE:
        logger.info(f"Padding firmware data from {len(FLASH)} to {EXPECTED_FLASH_SIZE} bytes")
        FLASH += b'\xFF' * (EXPECTED_FLASH_SIZE - len(FLASH))
    
    # Initialize progress tracking
    print_progress.start_time = time.time()
    
    # Create backup filename for potential use
    backup_path = f"{flash_path}.bak"
    
    # Use context manager for safer serial handling
    with safe_serial(port, baudrate, timeout=0.1, write_timeout=2) as ser:
        try:
            # Step 1: Verify firmware and prepare
            try:
                dataEndAddr = (FLASH[0x40] << 24) | (FLASH[0x41] << 16) | (FLASH[0x42] << 8) | FLASH[0x43]
                
                if dataEndAddr <= 0 or dataEndAddr > EXPECTED_FLASH_SIZE:
                    logger.error(f"Invalid data end address in header: {dataEndAddr:#x}")
                    return False
                    
                logger.info(f"Firmware size from header: {dataEndAddr} bytes")
            except IndexError:
                logger.error("Failed to extract data end address from header")
                return False
            
            # Calculate data blocks for progress tracking
            dataStartAddr = 0
            maxPos = 0
            dataAddr = dataStartAddr
            while dataAddr < dataEndAddr:
                remainder = dataAddr % BLOCK_SIZE
                realLen = (BLOCK_SIZE - remainder) if (dataAddr + BLOCK_SIZE <= dataEndAddr) else (dataEndAddr - dataAddr)
                maxPos += 1
                dataAddr += realLen

            # Step 2: Open port and handshake
            logger.info("Initiating handshake with radio")
            ser.reset_input_buffer()
            ser.reset_output_buffer()
            ser.write(CMD_DOWNLOAD)
            
            try:
                rxBuf = read_exact(ser, 8, timeout=10, description="handshake response")
            except TimeoutError:
                logger.error("No response from radio. Is the radio in update mode?")
                print("\nERROR: No response from radio. Please check:")
                print("1. Radio is in update mode (hold SK1+PTT+SK2 while powering on)")
                print("2. Cable is properly connected")
                print("3. Correct serial port is selected")
                return False
                
            data = bytearray(8)
            data[:8] = FLASH[0x30:0x30+8]
            
            # Determine radio type
            if data[1] == ord('2'):
                # New hardware
                if rxBuf[:len(CMD_NEW_HARDWARE)] != CMD_NEW_HARDWARE:
                    logger.error(f"Handshake failed (new hardware): expected {CMD_NEW_HARDWARE!r}, got {rxBuf[:len(CMD_NEW_HARDWARE)]!r}")
                    return False
                logger.info("Connected to new hardware radio")
            else:
                # Old hardware
                if rxBuf[:len(CMD_UPDATE)] != CMD_UPDATE:
                    logger.error(f"Handshake failed (old hardware): expected {CMD_UPDATE!r}, got {rxBuf[:len(CMD_UPDATE)]!r}")
                    return False
                logger.info("Connected to old hardware radio")

            # Step 3: ACK
            logger.info("Sending initial ACK")
            ser.reset_input_buffer()
            ser.write(CMD_ACK[:1])
            try:
                response = read_exact(ser, 1, description="ACK response")
                if response != CMD_ACK[:1]:
                    logger.error(f"ACK failed: expected {CMD_ACK[:1]!r}, got {response!r}")
                    return False
            except TimeoutError:
                logger.error("Timeout waiting for ACK response")
                return False

            # Step 4: Send FLASH ERASE command
            logger.info("Erasing flash memory - DO NOT INTERRUPT OR POWER OFF")
            data = bytearray(16)
            data[:8] = CMD_FLASH[1]
            data[8:16] = bytes([40, 6, 136, 25, 19, 3, 24, 32])
            
            # Use retry for critical commands
            if not write_and_wait_ack(ser, data, CMD_ACK[:1], description="FLASH ERASE command", retry_count=2):
                logger.error("Flash erase failed")
                return False

            # Step 5: Send PROGRAM1 command
            logger.info("Sending PROGRAM command")
            data = bytearray(8)
            data[:8] = CMD_PRG
            
            if not write_and_wait_ack(ser, data, CMD_ACK[:1], description="PROGRAM1 command", retry_count=1):
                logger.error("Program command failed")
                return False

            # Step 6: Send firmware data in blocks
            logger.info("Beginning firmware upload")
            check_sum = 0
            curTimes = 80  # Starting offset in firmware file
            dataAddr = dataStartAddr
            pos = 0
            
            # Backup storage for failed blocks to retry
            failed_blocks = []
            
            while dataAddr < dataEndAddr:
                remainder = dataAddr % BLOCK_SIZE
                realLen = (BLOCK_SIZE - remainder) if (dataAddr + BLOCK_SIZE <= dataEndAddr) else (dataEndAddr - dataAddr)
                
                # Check for data overrun
                if curTimes + realLen > len(FLASH):
                    logger.error(f"Data overrun: curTimes={curTimes}, realLen={realLen}, FLASH size={len(FLASH)}")
                    return False
                
                # Prepare block header
                realData = bytearray(realLen + 5)
                realData[0] = (dataAddr >> 24) & 0xFF
                realData[1] = (dataAddr >> 16) & 0xFF
                realData[2] = (dataAddr >> 8) & 0xFF
                realData[3] = dataAddr & 0xFF
                realData[4] = 0
                
                # Get block data
                data = FLASH[curTimes:curTimes+realLen]
                realData[5:] = data
                check_sum += sum(data)
                curTimes += realLen
                
                # Write block header
                ser.write(realData[:5])
                # Write block data
                ser.write(realData[5:])
                
                try:
                    response = read_exact(ser, 1, timeout=5, description=f"block at {dataAddr:#x}")
                    if response != CMD_ACK[:1]:
                        logger.error(f"Block write failed at {dataAddr:#x}: expected {CMD_ACK[:1]!r}, got {response!r}")
                        # Record failed block for potential retry
                        failed_blocks.append((dataAddr, realLen, curTimes - realLen))
                        if len(failed_blocks) > 5:
                            logger.error("Too many block write failures, aborting")
                            return False
                except TimeoutError:
                    logger.error(f"Timeout waiting for ACK after block write at {dataAddr:#x}")
                    # Record failed block for potential retry
                    failed_blocks.append((dataAddr, realLen, curTimes - realLen))
                    if len(failed_blocks) > 5:
                        logger.error("Too many block timeouts, aborting")
                        return False
                    
                pos += 1
                print_progress(pos * 100 / maxPos, f"Writing block at {dataAddr:#x}")
                dataAddr += realLen

            # Retry failed blocks if any
            if failed_blocks:
                logger.warning(f"Retrying {len(failed_blocks)} failed blocks")
                for addr, length, time_pos in failed_blocks:
                    logger.info(f"Retrying block at {addr:#x}")
                    
                    # Prepare block header for retry
                    retryData = bytearray(length + 5)
                    retryData[0] = (addr >> 24) & 0xFF
                    retryData[1] = (addr >> 16) & 0xFF
                    retryData[2] = (addr >> 8) & 0xFF
                    retryData[3] = addr & 0xFF
                    retryData[4] = 0
                    
                    # Get retry block data
                    retry_data = FLASH[time_pos:time_pos+length]
                    retryData[5:] = retry_data
                    
                    # Write retry block
                    ser.write(retryData[:5])
                    ser.write(retryData[5:])
                    
                    try:
                        retry_resp = read_exact(ser, 1, timeout=5, description=f"retry block at {addr:#x}")
                        if retry_resp != CMD_ACK[:1]:
                            logger.error(f"Retry failed for block at {addr:#x}")
                            return False
                        logger.info(f"Successfully retried block at {addr:#x}")
                    except TimeoutError:
                        logger.error(f"Timeout on retry for block at {addr:#x}")
                        return False

            # Step 7: Send END and checksum
            logger.info("Sending END command and checksum")
            data = bytearray(9)
            data[0:3] = b'END'
            data[3:5] = b'\xFF\xFF'
            data[5] = (check_sum >> 24) & 0xFF
            data[6] = (check_sum >> 16) & 0xFF
            data[7] = (check_sum >> 8) & 0xFF
            data[8] = check_sum & 0xFF
            
            # Use retry for critical END command
            if not write_and_wait_ack(ser, data, CMD_ACK[:1], timeout=15, description="END command", retry_count=2):
                logger.error("END/Checksum verification failed")
                return False

            logger.info("Firmware update completed successfully!")
            print("\nFirmware update successful! You can safely disconnect the radio.")
            return True
            
        except KeyboardInterrupt:
            logger.warning("Update cancelled by user")
            print("\nOperation cancelled - radio may be in an inconsistent state")
            return False
        except Exception as e:
            logger.exception(f"Unexpected error during update: {e}")
            print(f"\nError: {e}")
            return False

# --- Entry Point ---
if __name__ == "__main__":
    import argparse
    parser = argparse.ArgumentParser(description="Baofeng Radio Firmware Updater")
    parser.add_argument("--port", required=True, help="Serial port (e.g. COM3 or /dev/ttyUSB0)")
    parser.add_argument("--baud", type=int, default=115200, help="Baudrate (default: 115200)")
    parser.add_argument("--flash", required=True, help="Path to firmware file")
    parser.add_argument("--verbose", "-v", action="store_true", help="Enable verbose logging")
    parser.add_argument("--force", "-f", action="store_true", help="Skip firmware validation checks")
    parser.add_argument("--list-ports", action="store_true", help="List available serial ports and exit")
    args = parser.parse_args()
    
    try:
        # Special handling for --list-ports
        if args.list_ports:
            import serial.tools.list_ports
            ports = list(serial.tools.list_ports.comports())
            print("Available serial ports:")
            for port in ports:
                print(f"  {port.device}: {port.description}")
            sys.exit(0)
            
        success = updater(args.port, args.baud, args.flash, args.verbose, args.force)
        sys.exit(0 if success else 1)
    except KeyboardInterrupt:
        print("\nOperation cancelled by user")
        sys.exit(1)
    except Exception as e:
        print(f"\nCritical error: {e}")
        sys.exit(1)