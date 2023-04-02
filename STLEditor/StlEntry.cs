using Gibbed.IO;
using System;
using System.IO;

namespace STLEditor
{
    /*
    struct StlEntry	// sizeof 0x50
    {	
        0x00	DWord unknown1[2];	// always 0x00000000
        0x08	DWord string1offset;	// file offset for string1 (non-NLS key)
        0x0C	DWord string1size;	// size of string1
        0x10	DWord unknown2[2];	// always 0x00000000
        0x18	DWord string2offset;	// file offset for string2
        0x1C	DWord string2size;	// size of string2
        0x20	DWord unknown3[2];	// always 0x00000000
        0x28	DWord string3offset;	// file offset for string3
        0x2C	DWord string3size;	// size of string3
        0x30	DWord unknown4[2];	// always 0x00000000
        0x38	DWord string4offset;	// file offset for string4
        0x3C	DWord string4size;	// size of string4
        0x40	DWord unknown5;	// always 0xFFFFFFFF
        0x44	DWord unknown6[3];	// always 0x00000000
    }
    */

    public class _StlEntry
    {
        public String string1;
        public String string2;
        public String string3;
        public String string4;
    }

    public class StlEntry
    {

        public int[] unknown1;
        public int string1offset;
        public int string1size;
        //--
        public int[] unknown2;
        public int string2offset;
        public int string2size;
        //--
        public int[] unknown3;
        public int string3offset;
        public int string3size;
        //--
        public int[] unknown4;
        public int string4offset;
        public int string4size;
        //--
        public int unknown5;
        public int[] unknown6;

        public StlEntry(Stream stream)
        {
            unknown1 = new int[2];
            for (int i = 0; i < 2; i++)
            {
                unknown1[i] = stream.ReadValueS32();
            }
            string1offset = stream.ReadValueS32();
            string1size = stream.ReadValueS32();

            unknown2 = new int[2];
            for (int i = 0; i < 2; i++)
            {
                unknown2[i] = stream.ReadValueS32();
            }
            string2offset = stream.ReadValueS32();
            string2size = stream.ReadValueS32();

            unknown3 = new int[2];
            for (int i = 0; i < 2; i++)
            {
                unknown3[i] = stream.ReadValueS32();
            }
            string3offset = stream.ReadValueS32();
            string3size = stream.ReadValueS32();

            unknown4 = new int[2];
            for (int i = 0; i < 2; i++)
            {
                unknown4[i] = stream.ReadValueS32();
            }
            string4offset = stream.ReadValueS32();
            string4size = stream.ReadValueS32();

            unknown5 = stream.ReadValueS32();
            unknown6 = new int[3];
            for (int i = 0; i < 3; i++)
            {
                unknown6[i] = stream.ReadValueS32();
            }
        }
    }
}
