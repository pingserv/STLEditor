using System;
using System.Runtime.InteropServices;

namespace STLEditor.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Signature
    {
        public uint MagicNumber;
        public ushort fileTypeId;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] pad1;
    }
}
