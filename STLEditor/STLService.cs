using STLEditor.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace STLEditor
{
    public static class STLService
    {
        public static string filename;
        public static StringList stringList;

        public static bool isFileValid()
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show("The file does not exist.", "File opening error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                if (!fs.CanRead)
                {
                    MessageBox.Show("The file does not readable.", "File opening error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (fs.Length < 56)
                {
                    MessageBox.Show("Invalid file.", "File opening error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (fs.ReadValueU32() != 0xDEADBEEF)
                {
                    MessageBox.Show("It's not a StringList file.", "File opening error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }

        public static void openFile()
        {
            byte[] data = File.ReadAllBytes(filename);
            MemoryStream stream = new MemoryStream(data);
            
            stringList = new StringList(stream);
        }

        public static void saveFile()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                writer.BaseStream.Position = 0;
                writer.BaseStream.WriteStructure(stringList.stlFileStruct.Signature);
                writer.BaseStream.WriteStructure(stringList.stlFileStruct.Header);

                foreach (StlEntry entry in stringList.stlFileStruct.Entries)
                {
                    writer.BaseStream.WriteStructure(entry);
                }
                
                for (int i = 0; i < stringList.stlFileStruct.Records.Length; i++)
                {
                    StlRecord record = stringList.stlFileStruct.Records[i];
                    long mod = writer.BaseStream.Position % 8;

                    if (i > 0 && mod == 0)
                    {
                        writer.BaseStream.Position += 8;
                    }

                    if (mod > 0)
                    {
                        writer.BaseStream.Position += 8-mod;
                    }

                    stringList.stlFileStruct.Entries[i].Key = new Item()
                    {
                        columnOffset = (int)writer.BaseStream.Position - 0x10,
                        columnSize = record.Key.Length+1
                    };

                    writer.BaseStream.WriteString(record.Key);

                    mod = writer.BaseStream.Position % 8;

                    if (mod == 0)
                    {
                        writer.BaseStream.Position += 8;
                    }

                    if (mod > 0)
                    {
                        writer.BaseStream.Position += 8-mod;
                    }

                    stringList.stlFileStruct.Entries[i].Value = new Item()
                    {
                        columnOffset = (int)writer.BaseStream.Position - 0x10,
                        columnSize = record.Value.Length+1
                    };

                    writer.BaseStream.WriteString(record.Value);
                }

                writer.BaseStream.WriteByte(new byte());

                writer.BaseStream.Position = Marshal.SizeOf(typeof(Signature)) + Marshal.SizeOf(typeof(StlHeader));
                foreach (StlEntry entry in stringList.stlFileStruct.Entries)
                {
                    writer.BaseStream.WriteStructure(entry);
                }
            }
        }

        internal static void closeFile()
        {
            filename = null;
            stringList = null;
        }
    }
}
