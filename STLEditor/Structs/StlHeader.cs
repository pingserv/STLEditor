using System;
using System.Runtime.InteropServices;

namespace STLEditor.Structs
{
    /*
     * typedef struct {
	    int32_t stlFileId;	// Stl file Id
	    int32_t unknown1[5];	// always 0x00000000
	    int32_t headerSize;	// size (in bytes) of the StlHeader? (always 0x00000028)
	    int32_t entriesSize;	// size (in bytes) of the StlEntries
	    int32_t unknown2[2];	// always 0x00000000
    } StlHeader;
    */

    public struct StlHeader
    {
        public ushort stlFileId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] pad2;

        public int entrySize { get; set; }
        public int entriesCount { get; set; }

       
        public byte[] pad3;
    }
}
