using System.Runtime.InteropServices;

namespace STLEditor.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Item
    {
        public int columnOffset; // Int32 Little Edian (DCBA)
        public int columnSize;   // Int32 Little Edian (DCBA)
    }
}
