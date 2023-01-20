import socket
import sys

host = "localhost"  # Typically localhost or 127.0.0.1
port = 5001  # Typically 5001 for STK

# Attempt to create the socket
try:
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((host, port))
except (socket.error):
    sock.close()
    print("Could not open socket - Please start STK first")
    sys.exit(1)

message = "New / */Satellite NewSat\n"  # Connect commands must be terminated with a newline char
sock.sendall(message.encode())

# Terminate
sock.close()
