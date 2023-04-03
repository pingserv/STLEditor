using System.Runtime.InteropServices;

namespace STLEditor
{
    /*
    struct StlHeader	// sizeof 0x28
    {	
        0x00	DWord stlFileId;	// Stl file Id
        0x04	DWord unknown1[5];	// always 0x00000000
        0x08	DWord headerSize;	// size (in bytes) of the StlHeader? (always 0x00000028)
        0x0C	DWord entriesSize;	// size (in bytes) of the StlEntries
        0x04	DWord unknown2[2];	// always 0x00000000
    }
    */

    public struct StlHeader
    {
        public int stlFileId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] unknown1;

        public int headerSize;
        public int entriesSize;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown2;
    }
}
