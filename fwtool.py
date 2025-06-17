#!/usr/bin/env python3
import argparse
import os
import struct

# Constants for Baofeng firmware
XOR_KEY = 200
OFFSET = 80

def check_encryption_status(data):
    """
    Check if the firmware is encrypted or decrypted by examining byte 0x53.
    
    Returns:
        tuple: (status, message) where status is 'encrypted', 'decrypted', or 'unknown'
    """
    if len(data) <= 0x53:
        return "unknown", "File too small to determine encryption status"
    
    marker_byte = data[0x53]
    if marker_byte == 0x20:
        return "decrypted", "File appears to be decrypted (marker byte 0x53 = 0x20)"
    elif marker_byte == 0xE8:  # 0x20 XOR 0xC8 (200)
        return "encrypted", "File appears to be encrypted (marker byte 0x53 = 0xE8)"
    else:
        return "unknown", f"Unknown encryption status (marker byte 0x53 = 0x{marker_byte:02X})"

def validate_firmware_header(data):
    """
    Validate Baofeng firmware header structure.
    
    Header should have:
    - 0x00: "BaoFeng" (0xFF padded)
    - 0x10: "BF_5RH" (0xFF padded)
    - 0x20: "V1.0.0.0" (0xFF padded)
    - 0x30: May be "V2.0.0.0" (0xFF padded)
    - 0x40: Size of file minus the 0x50 header
    
    Returns:
        tuple: (is_valid, message)
    """
    # Check minimum length
    if len(data) < 0x50:  # Need at least the full header
        return False, "File too small to possibly be valid firmware"
    
    # Check "BaoFeng" signature at 0x0
    if not (data[0:7] == b'BaoFeng' and all(b == 0xFF for b in data[7:16])):
        return False, "Invalid header: Missing 'BaoFeng' signature"
    
    # Check "BF_5RH" at 0x10
    if not (data[16:22] == b'BF_5RH' and all(b == 0xFF for b in data[22:32])):
        return False, "Invalid header: Missing 'BF_5RH' model signature"
    
    # Check "V1.0.0.0" at 0x20
    if not (data[32:40] == b'V1.0.0.0' and all(b == 0xFF for b in data[40:48])):
        return False, "Invalid header: Missing version signature"
    
    # Check size field at 0x40
    size_field = struct.unpack('>I', data[64:68])[0]  # Big-endian 32-bit int
    actual_size = len(data) - OFFSET
    
    # Validate size field
    if size_field != actual_size:
        return False, f"Size mismatch: Header claims {size_field} bytes, actual data size is {actual_size} bytes"
    
    return True, "Valid Baofeng firmware header"

def process_file(input_file, output_file=None, mode=None):
    """
    Encrypt or decrypt a firmware file using XOR.
    
    Args:
        input_file: Path to input file
        output_file: Path to output file (optional)
        mode: 'encrypt' or 'decrypt' (optional, detected from file extension if None)
    """
    # Determine mode if not specified
    if mode is None:
        if input_file.lower().endswith('.dat'):
            mode = 'decrypt'
        elif input_file.lower().endswith('.bin'):
            mode = 'encrypt'
        else:
            print(f"Error: Cannot determine mode from file extension '{os.path.splitext(input_file)[1]}'")
            print("Please specify mode or use .bin/.dat extension")
            return
    
    # Determine output file if not specified
    if output_file is None:
        base = os.path.splitext(input_file)[0]
        output_file = f"{base}.bin" if mode == 'decrypt' else f"{base}.dat"
    
    try:
        # Read input file
        with open(input_file, 'rb') as f_in:
            data = bytearray(f_in.read())
        
        # Check encryption status before processing
        status, message = check_encryption_status(data)
        print(f"File status: {message}")
        
        # Check if we're trying to encrypt an already encrypted file
        # or decrypt an already decrypted file
        if mode == 'encrypt' and status == 'encrypted':
            print("Error: Cannot encrypt - file is already encrypted")
            return
        elif mode == 'decrypt' and status == 'decrypted':
            print("Error: Cannot decrypt - file is already decrypted")
            return
        
        # Validate header before processing
        is_valid, message = validate_firmware_header(data)
        if not is_valid:
            print(f"Warning: {message}")
            response = input("Continue anyway? (y/n): ")
            if response.lower() != 'y':
                print("Operation cancelled.")
                return
                
        # XOR each byte starting from offset
        for i in range(OFFSET, len(data)):
            data[i] ^= XOR_KEY
        
        # Write modified data to output file
        with open(output_file, 'wb') as f_out:
            f_out.write(data)
            
        print(f"Success: {mode.capitalize()}ed firmware from '{input_file}' to '{output_file}'")
        print(f"Processed {len(data)-OFFSET} bytes starting at offset {OFFSET}")
        
    except Exception as e:
        print(f"Error: {str(e)}")

def main():
    parser = argparse.ArgumentParser(description='Encrypt or decrypt Baofeng firmware files')
    parser.add_argument('input_file', help='Input firmware file (.bin or .dat)')
    parser.add_argument('output_file', nargs='?', help='Output file (optional)')
    parser.add_argument('--mode', choices=['encrypt', 'decrypt'], 
                        help='Force encryption or decryption mode (default: based on file extension)')
    
    args = parser.parse_args()
    process_file(args.input_file, args.output_file, args.mode)

if __name__ == '__main__':
    main()