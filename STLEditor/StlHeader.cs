using Gibbed.IO;
using System.IO;

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

    public class StlHeader
    {
        public int stlFileId;
        public int[] unknown1;
        public int headerSize;
        public int entriesSize;
        public int[] unknown2;

        public StlHeader(Stream stream)
        {
            stlFileId = stream.ReadValueS32();

            unknown1 = new int[5];
            for (int i = 0; i < 5; i++)
            {
                unknown1[i] = stream.ReadValueS32();
            }

            headerSize = stream.ReadValueS32();
            entriesSize = stream.ReadValueS32();

            unknown2 = new int[2];
            for (int i = 0; i < 2; i++)
            {
                unknown2[i] = stream.ReadValueS32();
            }
        }

    }
}
