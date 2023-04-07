using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace STLEditor.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BinaryFileStruct
    {
        public Signature Signature;
        public StlHeader Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public StlEntry[] Entries;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public StlRecord[] Records;

        public int getStructSize()
        {
            int sum = 0;
            foreach (StlRecord record in Records)
            {
                sum += (record.Key.Length + record.Value.Length) * 2;
            }

            return sum;
        }
    }
}
