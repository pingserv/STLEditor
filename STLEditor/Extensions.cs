using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STLEditor
{
    static class Extensions
    {
        public static int ReadInt32BE(this BinaryReader reader)
        {
            int val = reader.ReadInt32();
            int ret = (val >> 24 & 0xFF) << 0;
            ret |= (val >> 16 & 0xFF) << 8;
            ret |= (val >> 8 & 0xFF) << 16;
            ret |= (val >> 0 & 0xFF) << 24;
            return ret;
        }
    }
}
