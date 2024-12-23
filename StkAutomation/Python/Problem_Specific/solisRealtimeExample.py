from struct import Struct
import binascii
import struct

# Data is a raw packet from framer
data = b'D\x16\x98\x12\x00\x00\xe8\x03\x00\x00\x00\x00\xee\xee\xee\xeeUz\x8c\xbd\x00\xda\x1b?\xc6)\x19\xbe\xcd\xacF?\xc9\xb2\xc3\xb8\x0e\xe8\xc19.\x99\x108\x00\x00\x00\x00Uz\x8c\xbd\x00\xda\x1b?\xc6)\x19\xbe\xcd\xacF?\xc9\xb2\xc3\xb8\x0e\xe8\xc19.\x99\x108\x00\x00\x00\x00\x00\x00\x00\x00\xf0?\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\xf0?'

# The first two bytes of data represents the apid number
apid_struct = Struct(f"<H")
apid_num = int(apid_struct.unpack(data[0:2])[0])
# apid_num = 5700
print('apid num is', apid_num)

# The next 6 bytes represent the SCLK timestamp
date_parts_struct = Struct('<IH')
nowtime = date_parts_struct.unpack(data[2:8])
ctime = float(nowtime[0]) + float(nowtime[1]/40000)
print('sclk timestamp', ctime)

# The remainder of the packet is the apid data specified by the tlmdb
# Channel values are in the order specified by FswTlmDetail.csv
apid_data_struct = Struct('<I4x7fI7f2B6d')
channel_values = apid_data_struct.unpack(data[8:])
print('channel values', channel_values)


# Command is CMD_TLM_SET_APID_FREQ - 100000 from cmd_master
mnem = '100000'
mnem = binascii.unhexlify(mnem)
raw_command = struct.pack("<I", int.from_bytes(mnem, byteorder='big', signed=False))

# Now check the CmdDetail for the parameters. We have one 8 byte double and one 4 byte unsigned int for this one.
raw_command += struct.pack("<dI", 1, 5700)
print('raw_command', raw_command)
# b'\x00\x00\x10\x00\x00\x00\x00\x00\x00\x00\xf0?D\x16\x00\x00'
