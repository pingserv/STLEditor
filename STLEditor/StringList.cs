using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        public List<DatagridEntry> entries = new List<DatagridEntry>();
        private List<StlEntry> _entries = new List<StlEntry>();

        public StringList(Stream stream)
        {
            stream.Position += 0x10;
            header = stream.ReadStructure<StlHeader>();

            // if (!HeaderIds.Contains(header.stlFileId))
            //   return;

            int entries = header.entriesSize / 0x28;
            for (int i = 0; i < entries; i++)
            {
                _entries.Add(stream.ReadStructure<StlEntry>());
            }

            foreach (StlEntry entry in _entries)
            {
                DatagridEntry _entry = new DatagridEntry();
                _entry.Id = entry.id.ToString();

                stream.Position = entry.key.columnOffset + 0x10;
                _entry.Key = stream.ReadString((uint)entry.key.columnSize - 1);
                
                stream.Position = entry.value.columnOffset + 0x10;
                _entry.Value = stream.ReadString((uint)entry.value.columnSize - 1);
                
                this.entries.Add(_entry);
            }
        }
    }
}