import argparse
import serial
import time
import re
import sys
import os

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

def update_font_data(port, baud, font_buffer, verbose=False):
    BLOCK_SIZE = 4096
    CHUNK_SIZE = 1024
    DATA_SIZE = 458752
    CMD_AUDIO = b'Font'
    CMD_END = b'END\x00'
    CMD_ACK = 0x41  # 'A'

    try:
        with serial.Serial(
            port=port,
            baudrate=baud,
            timeout=5,
            write_timeout=1,
            dsrdtr=True,
            rtscts=True
        ) as spt:
            if verbose:
                print(f"Opened port {port} at {baud} baud.")

            # Handshake
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
                    if verbose:
                        print("Handshake response received.")
                    break
            else:
                print("Handshake failed.")
                sys.exit(2)

            time.sleep(0.2)
            spt.reset_input_buffer()

            # Send 'Font' command
            spt.write(CMD_AUDIO)
            spt.write(b'\xFF' * 4)
            if verbose:
                print("Sent 'Font' command and 4x 0xFF.")
            # Wait for 1 byte response
            while True:
                resp = spt.read(1)
                if len(resp) == 1:
                    if verbose:
                        print("Received response after 'Font' command.")
                    break
                time.sleep(0.02)
            spt.reset_input_buffer()

            # Data transfer
            data_addr = 0
            cur_times = 0
            while data_addr < DATA_SIZE:
                real_len = min(BLOCK_SIZE, DATA_SIZE - data_addr)
                real_data = font_buffer[cur_times:cur_times+real_len]
                # Pad to 4096 bytes if needed
                if len(real_data) < BLOCK_SIZE:
                    real_data += b'\xFF' * (BLOCK_SIZE - len(real_data))
                # Write in 1024-byte chunks
                for k in range(0, BLOCK_SIZE, CHUNK_SIZE):
                    spt.write(real_data[k:k+CHUNK_SIZE])
                if verbose:
                    print(f"Sent data block at address {data_addr}.")
                # Wait for ACK
                while True:
                    resp = spt.read(1)
                    if len(resp) == 1:
                        break
                    time.sleep(0.02)
                if resp[0] != CMD_ACK:
                    print(f"ACK not received after data block at address {data_addr}. Got: {resp.hex()}")
                    sys.exit(3)
                data_addr += real_len
                cur_times += real_len

            # Send END block
            end_block = bytearray(BLOCK_SIZE)
            end_block[0:3] = b'END'
            end_block[3:] = b'\xFF' * (BLOCK_SIZE - 3)
            for k in range(0, BLOCK_SIZE, CHUNK_SIZE):
                spt.write(end_block[k:k+CHUNK_SIZE])
            if verbose:
                print("Sent END block.")
            # Wait for ACK
            while True:
                resp = spt.read(1)
                if len(resp) == 1:
                    break
                time.sleep(0.02)
            if resp[0] != CMD_ACK:
                print(f"ACK not received after END block. Got: {resp.hex()}")
                sys.exit(4)

            print("Font data update successful.")

    except serial.SerialException as e:
        print(f"Serial error: {e}")
        sys.exit(5)

def main():
    parser = argparse.ArgumentParser(description="Update radio font data via serial port (text format).")
    parser.add_argument('--port', required=True, help='Serial port (e.g. COM3 or /dev/ttyUSB0)')
    parser.add_argument('--font', required=True, help='Font text file')
    parser.add_argument("--baud", type=int, default=115200, help="Baudrate (default: 115200)")
    parser.add_argument('--verbose', action='store_true', help='Enable verbose output')
    args = parser.parse_args()

    if not os.path.isfile(args.font):
        print(f"Font file not found: {args.font}")
        sys.exit(1)

    font_buffer = parse_font_file(args.font)
    update_font_data(args.port, args.baud, font_buffer, args.verbose)

if __name__ == "__main__":
    main()