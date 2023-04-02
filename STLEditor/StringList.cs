using System.Collections.Generic;
using System.IO;
using Gibbed.IO;

namespace STLEditor
{
    public class StringList
    {
        private List<int> HeaderIds = new List<int>() { 
            0x0000C91A, // 51482 Conversation
            0x0000CB28, // 52008 Items
            0x0000C918 // 51480 AttributeDescriptions
        };
        
        public StlHeader header;
        List<StlEntry> _entries;
        public List<_StlEntry> entries = new List<_StlEntry>();

        public StringList(Stream stream)
        {
            this._entries = new List<StlEntry>();
            this.entries = new List<_StlEntry>();

            stream.Position += 0x10;
            header = new StlHeader(stream);

           // if (!HeaderIds.Contains(header.stlFileId))
             //   return;

            int entries = header.entriesSize / 0x50;
            for (int i = 0; i < entries; i++)
            {
                this._entries.Add(new StlEntry(stream));
            }
            
            foreach (StlEntry entry in this._entries)
            {
                _StlEntry _entry = new _StlEntry();
                stream.Position = entry.string1offset + 0x10;
                _entry.string1 = stream.ReadString((uint)entry.string1size - 1);

                stream.Position = entry.string2offset + 0x10;
                _entry.string2 = stream.ReadString((uint)entry.string2size - 1);

                stream.Position = entry.string3offset + 0x10;
                _entry.string3 = stream.ReadString((uint)entry.string3size);

                stream.Position = entry.string4offset + 0x10;
                _entry.string4 = stream.ReadString((uint)entry.string4size);

                this.entries.Add(_entry);
            }
        }
    }
}