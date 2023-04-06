using System.Runtime.InteropServices;

namespace STLEditor
{
    public struct StlRecord
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] unknown1;

        public string Key;
        public string Value;
    }
}