using System.Runtime.InteropServices;

namespace STLEditor.Structs
{
    /*
    typedef struct {
	    int32_t unknown1[2];	// always 0x00000000
	    int32_t string1offset;	// file offset for string1 (non-NLS key)
	    int32_t string1size;	// size of string1
	    int32_t unknown2[2];	// always 0x00000000
	    int32_t string2offset;	// file offset for string2
	    int32_t string2size;	// size of string2
	    int32_t unknown3[2];	// always 0x00000000
    } StlEntry;
    */

    [StructLayout(LayoutKind.Sequential)]
    public struct StlEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown1;

        public Item Key;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown2;

        public Item Value;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown3;
    }
}