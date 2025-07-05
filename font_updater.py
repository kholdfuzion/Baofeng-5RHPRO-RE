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

import argparse
import serial
import time
import re
import sys
import os
import logging
from contextlib import contextmanager

# --- Setup Logging ---
def setup_logging(verbose=False, log_file='font_update.log'):
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
    return logging.getLogger('font_updater')

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

def parse_font_file(path):
    """Parse a font text file into a bytes buffer (like C# btnChineseOpen_Click)."""
    buffer = bytearray()
    hex_pattern = re.compile(r'0x([0-9a-fA-F]{2})')
    with open(path, encoding='utf-8', errors='ignore') as f:
        for line in f:
            for match in hex_pattern.finditer(line):
                buffer.append(int(match.group(1), 16))
    # Pad to 458752 bytes (C# Global.EEROM size)
    if len(buffer) < 458752:
        buffer += b'\xFF' * (458752 - len(buffer))
    elif len(buffer) > 458752:
        buffer = buffer[:458752]
    return buffer

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

def update_font_data(port, baud, font_buffer, verbose=False):
    """Update the radio's font data with improved error handling and reporting"""
    logger = setup_logging(verbose)
    logger.info(f"Starting font update on {port}")
    
    # Verify serial port
    if not verify_serial_port(port):
        logger.error(f"Serial port verification failed for {port}")
        print(f"Serial port verification failed for {port}")
        sys.exit(1)
    
    # Define constants
    BLOCK_SIZE = 4096
    CHUNK_SIZE = 1024
    DATA_SIZE = 458752
    CMD_AUDIO = b'Font'
    CMD_END = b'END\x00'
    CMD_ACK = 0x41  # 'A'

    # Initialize progress tracking
    print_progress.start_time = time.time()

    # Use safer serial connection
    with safe_serial(
        port=port,
        baudrate=baud,
        timeout=5,
        write_timeout=1,
        dsrdtr=True,
        rtscts=True
    ) as spt:
        try:
            logger.info(f"Opened port {port} at {baud} baud")
            if verbose:
                print(f"Opened port {port} at {baud} baud.")
                
            # Display radio mode instructions
            print("\nIMPORTANT:")
            print("The radio should be turned on normally (not in update mode)\n")
            
            proceed = input("Is the radio powered on in normal mode? (y/n): ").lower()
            if proceed != 'y':
                logger.info("Update cancelled by user - radio not in normal mode")
                print("Update cancelled")
                sys.exit(0)

            # Handshake
            logger.info("Starting handshake sequence")
            for i in range(25):
                handshake = bytearray(16)
                handshake[:12] = b'\x00' * 12
                handshake[12:16] = b'\xFF' * 4
                spt.write(handshake)
                if verbose:
                    print(f"Sent handshake ({i+1}/25)")
                time.sleep(0.2)
                resp = spt.read(1)
                if len(resp) == 1:
                    logger.info("Handshake successful")
                    if verbose:
                        print("Handshake response received.")
                    break
            else:
                logger.error("Handshake failed - no response from radio")
                print("Handshake failed. Please check:")
                print("1. Radio is powered ON in normal mode")
                print("2. Cable is properly connected")
                print("3. Verify correct serial port")
                sys.exit(2)

            time.sleep(0.2)
            spt.reset_input_buffer()

            # Send 'Font' command
            logger.info("Sending 'Font' command")
            spt.write(CMD_AUDIO)
            spt.write(b'\xFF' * 4)
            if verbose:
                print("Sent 'Font' command and 4x 0xFF.")
            # Wait for 1 byte response
            timeout_start = time.time()
            while True:
                resp = spt.read(1)
                if len(resp) == 1:
                    logger.debug("Received response after 'Font' command")
                    if verbose:
                        print("Received response after 'Font' command.")
                    break
                if time.time() - timeout_start > 5:
                    logger.error("No response after 'Font' command")
                    print("No response after 'Font' command. Update failed.")
                    sys.exit(3)
                time.sleep(0.02)
            spt.reset_input_buffer()

            # Data transfer
            logger.info("Beginning font data transfer")
            data_addr = 0
            cur_times = 0
            total_blocks = (DATA_SIZE + BLOCK_SIZE - 1) // BLOCK_SIZE
            current_block = 0
            
            # Print initial progress
            print_progress(0, "Starting font transfer")
            
            while data_addr < DATA_SIZE:
                current_block += 1
                real_len = min(BLOCK_SIZE, DATA_SIZE - data_addr)
                real_data = font_buffer[cur_times:cur_times+real_len]
                # Pad to 4096 bytes if needed
                if len(real_data) < BLOCK_SIZE:
                    real_data += b'\xFF' * (BLOCK_SIZE - len(real_data))
                # Write in 1024-byte chunks
                for k in range(0, BLOCK_SIZE, CHUNK_SIZE):
                    spt.write(real_data[k:k+CHUNK_SIZE])
                
                # Update progress display (always on same line)
                progress = (current_block * 100) / total_blocks
                print_progress(progress, f"Writing block {current_block}/{total_blocks}")
                
                # Log verbose info to file only, don't print to console during transfers
                if verbose:
                    logger.debug(f"Sent data block at address {data_addr}")
                
                # Wait for ACK with timeout
                timeout_start = time.time()
                while True:
                    resp = spt.read(1)
                    if len(resp) == 1:
                        break
                    if time.time() - timeout_start > 5:
                        logger.error(f"Timeout waiting for ACK at address {data_addr}")
                        print(f"\nTimeout waiting for ACK at address {data_addr}")
                        sys.exit(3)
                    time.sleep(0.02)
                
                if resp[0] != CMD_ACK:
                    logger.error(f"ACK not received after data block at address {data_addr}. Got: {resp.hex()}")
                    print(f"\nACK not received after data block at address {data_addr}. Got: {resp.hex()}")
                    sys.exit(3)
                
                logger.debug(f"Block at address {data_addr} acknowledged")
                data_addr += real_len
                cur_times += real_len

            # Mark progress as complete
            print_progress(100, "Font data transfer complete")
            print()  # Add newline after progress display
            
            logger.info("All data blocks sent, sending END block")

            # Send END block
            end_block = bytearray(BLOCK_SIZE)
            end_block[0:3] = b'END'
            end_block[3:] = b'\xFF' * (BLOCK_SIZE - 3)
            for k in range(0, BLOCK_SIZE, CHUNK_SIZE):
                spt.write(end_block[k:k+CHUNK_SIZE])
            if verbose:
                print("Sent END block.")
                
            # Wait for ACK with timeout
            logger.debug("Waiting for final ACK")
            timeout_start = time.time()
            while True:
                resp = spt.read(1)
                if len(resp) == 1:
                    break
                if time.time() - timeout_start > 5:
                    logger.error("Timeout waiting for final ACK")
                    print("\nTimeout waiting for final ACK")
                    sys.exit(4)
                time.sleep(0.02)
                
            if resp[0] != CMD_ACK:
                logger.error(f"ACK not received after END block. Got: {resp.hex()}")
                print(f"ACK not received after END block. Got: {resp.hex()}")
                sys.exit(4)

            logger.info("Font data update successful")
            print("\nFont data update successful!")

        except serial.SerialException as e:
            logger.exception(f"Serial error: {e}")
            print(f"\nSerial error: {e}")
            sys.exit(5)
        except KeyboardInterrupt:
            logger.warning("Update cancelled by user")
            print("\nUpdate cancelled by user")
            sys.exit(0)
        except Exception as e:
            logger.exception(f"Unexpected error: {e}")
            print(f"\nUnexpected error: {e}")
            sys.exit(6)

def main():
    parser = argparse.ArgumentParser(
        description="Update radio font data via serial port. Radio must be powered ON in normal mode."
    )
    parser.add_argument('--port', required=True, help='Serial port (e.g. COM3 or /dev/ttyUSB0)')
    parser.add_argument('--font', required=True, help='Font text file')
    parser.add_argument("--baud", type=int, default=115200, help="Baudrate (default: 115200)")
    parser.add_argument('--verbose', action='store_true', help='Enable verbose output')
    parser.add_argument('--list-ports', action='store_true', help='List available serial ports and exit')
    args = parser.parse_args()

    # Add list-ports option like in fw_updater.py
    if args.list_ports:
        import serial.tools.list_ports
        ports = list(serial.tools.list_ports.comports())
        print("Available serial ports:")
        for port in ports:
            print(f"  {port.device}: {port.description}")
        sys.exit(0)

    if not os.path.isfile(args.font):
        print(f"Font file not found: {args.font}")
        sys.exit(1)

    font_buffer = parse_font_file(args.font)
    update_font_data(args.port, args.baud, font_buffer, args.verbose)

if __name__ == "__main__":
    main()