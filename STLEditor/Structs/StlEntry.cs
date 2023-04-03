using System.Runtime.InteropServices;

namespace STLEditor.Structs
{
    /*
    struct StlEntry	// sizeof 0x50
    {	
        0x00	DWord unknown1[2];	// always 0x00000000
        0x08	DWord keyOffset;	// file offset for string1 (non-NLS key)
        0x0C	DWord keySize;	    // size of string1
        0x10	DWord unknown2[2];	// always 0x00000000
        0x18	DWord valueOffset;	// file offset for string2
        0x1C	DWord valueSize;	// size of string2
        0x1C	DWord id;	        // size of string2
        0x20	DWord unknown3[2];	// always 0x00000000
    }
    */

    public struct Item
    {
        public int columnOffset;
        public int columnSize;
    }

    public struct StlEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown1;

        public Item key;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown2;

        public Item value;
        public uint id;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] unknown3;
    }
}