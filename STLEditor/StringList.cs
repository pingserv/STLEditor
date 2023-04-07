using System.Collections.Generic;
using System.IO;
using System.Text;
using STLEditor.Enums;
using STLEditor.Structs;

namespace STLEditor
{
    public class StringList
    {
        public uint MAGIC = 0xDEADBEEF;
        public BinaryFileStruct stlFileStruct = new BinaryFileStruct();
        public FileTypes fileType;

        public StringList(Stream stream)
        {
            StreamHelpers.DefaultEncoding = Encoding.Latin1;

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
                for (int i = 0; i < entriesCount; i++)
                {
                    stream.Position = stlFileStruct.Entries[i].Key.columnOffset + 0x10;
                    string key = stream.ReadString((uint)stlFileStruct.Entries[i].Key.columnSize - 1);

                    stream.Position = stlFileStruct.Entries[i].Value.columnOffset + 0x10;
                    string value = stream.ReadString((uint)stlFileStruct.Entries[i].Value.columnSize - 1);

                    stlFileStruct.Records[i] = new StlRecord(){
                        Key = key,
                        Value = value
                    };
                }
            }
        }
    }
}