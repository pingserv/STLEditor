using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace STLEditor.Structs
{
    public struct FullStlFileStruct
    {
        public Signature Signature;
        public StlHeader Header;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst =346)]
        public StlEntry[] Entries;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 346)]
        public StlRecord[] Records;
    }
}
