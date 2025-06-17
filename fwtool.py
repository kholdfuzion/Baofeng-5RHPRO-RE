#!/usr/bin/env python3
import argparse
import os

# Constants for Baofeng firmware
XOR_KEY = 200
OFFSET = 80

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