using System;
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
                byte[] bytes = stringList.stlFileStruct.StructToByteArray();

                writer.BaseStream.WriteBytes(bytes);

            }
        }

        internal static void closeFile()
        {
            filename = null;
            stringList = null;
        }
    }
}
