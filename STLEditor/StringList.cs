using System.Collections.Generic;
using System.IO;
using STLEditor.Enums;
using STLEditor.Structs;

namespace STLEditor
{
    public class StringList
    {
        public uint MAGIC = 0xDEADBEEF;
        public FullStlFileStruct stlFileStruct;
        public FileTypes fileType;
        public List<DatagridEntry> entries = new List<DatagridEntry>();

        public StringList(Stream stream)
        {
            stlFileStruct.Signature = stream.ReadStructure<Signature>();

            if (!stlFileStruct.Signature.MagicNumber.Equals(MAGIC))
                return;

            if (stlFileStruct.Signature.fileTypeId.Equals((int)FileTypes.STL))
            {
                fileType = FileTypes.STL;
                stlFileStruct.Header = stream.ReadStructure<StlHeader>();

                if (stlFileStruct.Header.entriesCount == 0 || stlFileStruct.Header.entrySize == 0)
                    return;

                int entriesCount = stlFileStruct.Header.entriesCount / stlFileStruct.Header.entrySize;
                stlFileStruct.Entries = new StlEntry[entriesCount]; 
                for (int i = 0; i < entriesCount; i++)
                {
                    stlFileStruct.Entries[i] = stream.ReadStructure<StlEntry>();
                }

                stlFileStruct.Records = new StlRecord[entriesCount];
                // TODO: Most kellene feltölteni a szöveges tartalmat de a változó szöveghossz miatt szerintem nem lehet. manuálisan kell majd.
                int j = 0;

                foreach (StlEntry entry in stlFileStruct.Entries)
                {
                    stlFileStruct.Records[j] = new StlRecord();

                    stream.Position = entry.key.columnOffset + 0x10;
                    stlFileStruct.Records[j].Key = stream.ReadString((uint)entry.key.columnSize - 1);

                    stream.Position = entry.value.columnOffset + 0x10;
                    stlFileStruct.Records[j].Value = stream.ReadString((uint)entry.value.columnSize - 1);

                    DatagridEntry _entry = new DatagridEntry();
                    _entry.Id = entry.id.ToString();
                    _entry.Key = stlFileStruct.Records[j].Key;
                    _entry.Value = stlFileStruct.Records[j].Value;

                    entries.Add(_entry);
                    j++;
                }
            }
        }
    }
}