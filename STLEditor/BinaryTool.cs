using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace STLEditor
{
    public enum Endian
    {
        Little,
        Big
    }

    public readonly struct MD5Hash
    {
        public readonly ulong lowPart;
        public readonly ulong highPart;
    }

    public static class NumberHelpers
    {
        public static int Align(this int value, int align)
        {
            if (value == 0)
            {
                return value;
            }

            return value + (align - value % align) % align;
        }

        public static uint Align(this uint value, uint align)
        {
            if (value == 0)
            {
                return value;
            }

            return value + (align - value % align) % align;
        }

        public static long Align(this long value, long align)
        {
            if (value == 0)
            {
                return value;
            }

            return value + (align - value % align) % align;
        }

        public static ulong Align(this ulong value, ulong align)
        {
            if (value == 0)
            {
                return value;
            }

            return value + (align - value % align) % align;
        }

        public static short BigEndian(this short value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static ushort BigEndian(this ushort value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static int BigEndian(this int value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static uint BigEndian(this uint value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static long BigEndian(this long value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static ulong BigEndian(this ulong value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static float BigEndian(this float value)
        {
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                uint value2 = BitConverter.ToUInt32(bytes, 0).Swap();
                return BitConverter.ToSingle(BitConverter.GetBytes(value2), 0);
            }

            return value;
        }

        public static double BigEndian(this double value)
        {
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                ulong value2 = BitConverter.ToUInt64(bytes, 0).Swap();
                return BitConverter.ToDouble(BitConverter.GetBytes(value2), 0);
            }

            return value;
        }

        public static short LittleEndian(this short value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static ushort LittleEndian(this ushort value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static int LittleEndian(this int value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static uint LittleEndian(this uint value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static long LittleEndian(this long value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static ulong LittleEndian(this ulong value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return value.Swap();
            }

            return value;
        }

        public static float LittleEndian(this float value)
        {
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                uint value2 = BitConverter.ToUInt32(bytes, 0).Swap();
                return BitConverter.ToSingle(BitConverter.GetBytes(value2), 0);
            }

            return value;
        }

        public static double LittleEndian(this double value)
        {
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                ulong value2 = BitConverter.ToUInt64(bytes, 0).Swap();
                return BitConverter.ToDouble(BitConverter.GetBytes(value2), 0);
            }

            return value;
        }

        public static short RotateLeft(this short value, int count)
        {
            return (short)((ushort)value).RotateLeft(count);
        }

        public static ushort RotateLeft(this ushort value, int count)
        {
            return (ushort)((value << count) | (value >> 16 - count));
        }

        public static int RotateLeft(this int value, int count)
        {
            return (int)((uint)value).RotateLeft(count);
        }

        public static uint RotateLeft(this uint value, int count)
        {
            return (value << count) | (value >> 32 - count);
        }

        public static long RotateLeft(this long value, int count)
        {
            return (long)((ulong)value).RotateLeft(count);
        }

        public static ulong RotateLeft(this ulong value, int count)
        {
            return (value << count) | (value >> 64 - count);
        }

        public static short RotateRight(this short value, int count)
        {
            return (short)((ushort)value).RotateRight(count);
        }

        public static ushort RotateRight(this ushort value, int count)
        {
            return (ushort)((value >> count) | (value << 16 - count));
        }

        public static int RotateRight(this int value, int count)
        {
            return (int)((uint)value).RotateRight(count);
        }

        public static uint RotateRight(this uint value, int count)
        {
            return (value >> count) | (value << 32 - count);
        }

        public static long RotateRight(this long value, int count)
        {
            return (long)((ulong)value).RotateRight(count);
        }

        public static ulong RotateRight(this ulong value, int count)
        {
            return (value >> count) | (value << 64 - count);
        }

        public static short Swap(this short value)
        {
            ushort num = (ushort)value;
            ushort num2 = (ushort)((0xFFu & (uint)(num >> 8)) | (0xFF00u & (uint)(num << 8)));
            return (short)num2;
        }

        public static ushort Swap(this ushort value)
        {
            return (ushort)((0xFFu & (uint)(value >> 8)) | (0xFF00u & (uint)(value << 8)));
        }

        public static int Swap(this int value)
        {
            return (int)((0xFF & ((uint)value >> 24)) | (0xFF00 & ((uint)value >> 8))) | (0xFF0000 & (value << 8)) | (-16777216 & (value << 24));
        }

        public static uint Swap(this uint value)
        {
            return (0xFFu & (value >> 24)) | (0xFF00u & (value >> 8)) | (0xFF0000u & (value << 8)) | (0xFF000000u & (value << 24));
        }

        public static long Swap(this long value)
        {
            return (long)((0xFF & ((ulong)value >> 56)) | (0xFF00 & ((ulong)value >> 40)) | (0xFF0000 & ((ulong)value >> 24)) | (0xFF000000u & ((ulong)value >> 8))) | (0xFF00000000L & (value << 8)) | (0xFF0000000000L & (value << 24)) | (0xFF000000000000L & (value << 40)) | (-72057594037927936L & (value << 56));
        }

        public static ulong Swap(this ulong value)
        {
            return (0xFF & (value >> 56)) | (0xFF00 & (value >> 40)) | (0xFF0000 & (value >> 24)) | (0xFF000000u & (value >> 8)) | (0xFF00000000uL & (value << 8)) | (0xFF0000000000uL & (value << 24)) | (0xFF000000000000uL & (value << 40)) | (0xFF00000000000000uL & (value << 56));
        }

        public static float Swap(this float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            int value2 = BitConverter.ToInt32(bytes, 0).Swap();
            return BitConverter.ToSingle(BitConverter.GetBytes(value2), 0);
        }
    }

    public static class StreamHelpers
    {
        private static class EnumTypeCache
        {
            private static TypeCode TranslateType(Type type)
            {
                if (type.IsEnum)
                {
                    Type underlyingType = Enum.GetUnderlyingType(type);
                    TypeCode typeCode = Type.GetTypeCode(underlyingType);
                    switch (typeCode)
                    {
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                            return typeCode;
                    }
                }

                throw new ArgumentException("unknown enum type", "type");
            }

            public static TypeCode Get(Type type)
            {
                return TranslateType(type);
            }
        }

        public static Encoding DefaultEncoding = Encoding.ASCII;

        internal static bool ShouldSwap(Endian endian)
        {
            return endian switch
            {
                Endian.Little => !BitConverter.IsLittleEndian,
                Endian.Big => BitConverter.IsLittleEndian,
                _ => throw new ArgumentException("unsupported endianness", "endian"),
            };
        }

        public static MemoryStream ReadToMemoryStream(this Stream stream, long size, int buffer)
        {
            MemoryStream memoryStream = new MemoryStream();
            long num = size;
            byte[] array = new byte[buffer];
            while (num > 0)
            {
                int num2 = (int)Math.Min(num, array.Length);
                if (stream.Read(array, 0, num2) != num2)
                {
                    throw new EndOfStreamException();
                }

                memoryStream.Write(array, 0, num2);
                num -= num2;
            }

            memoryStream.Seek(0L, SeekOrigin.Begin);
            return memoryStream;
        }

        public static MemoryStream ReadToMemoryStream(this Stream stream, long size)
        {
            return stream.ReadToMemoryStream(size, 262144);
        }

        public static void WriteFromStream(this Stream stream, Stream input, long size, int buffer)
        {
            long num = size;
            byte[] array = new byte[buffer];
            while (num > 0)
            {
                int num2 = (int)Math.Min(num, array.Length);
                if (input.Read(array, 0, num2) != num2)
                {
                    throw new EndOfStreamException();
                }

                stream.Write(array, 0, num2);
                num -= num2;
            }
        }

        public static void WriteFromStream(this Stream stream, Stream input, long size)
        {
            stream.WriteFromStream(input, size, 262144);
        }

        public static byte[] ReadBytes(this Stream stream, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            byte[] array = new byte[length];
            int num = stream.Read(array, 0, length);
            if (num != length)
            {
                throw new EndOfStreamException();
            }

            return array;
        }

        public static byte[] ReadBytes(this Stream stream, uint length)
        {
            return stream.ReadBytes((int)length);
        }

        public static void WriteBytes(this Stream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }

        public static int ReadAligned(this Stream stream, byte[] buffer, int offset, int size, int align)
        {
            if (size == 0)
            {
                return 0;
            }

            int result = stream.Read(buffer, offset, size);
            int num = size % align;
            if (num > 0)
            {
                stream.Seek(align - num, SeekOrigin.Current);
            }

            return result;
        }

        public static void WriteAligned(this Stream stream, byte[] buffer, int offset, int size, int align)
        {
            if (size != 0)
            {
                stream.Write(buffer, offset, size);
                int num = size % align;
                if (num > 0)
                {
                    byte[] buffer2 = new byte[align - num];
                    stream.Write(buffer2, 0, align - num);
                }
            }
        }

        public static bool ReadValueBoolean(this Stream stream)
        {
            return stream.ReadValueB8();
        }

        public static void WriteValueBoolean(this Stream stream, bool value)
        {
            stream.WriteValueB8(value);
        }

        public static bool ReadValueB8(this Stream stream)
        {
            return stream.ReadValueU8() > 0;
        }

        public static void WriteValueB8(this Stream stream, bool value)
        {
            stream.WriteValueU8((byte)(value ? 1u : 0u));
        }

        public static bool ReadValueB32(this Stream stream, Endian endian)
        {
            return stream.ReadValueU32(endian) != 0;
        }

        public static bool ReadValueB32(this Stream stream)
        {
            return stream.ReadValueB32(Endian.Little);
        }

        public static void WriteValueB32(this Stream stream, bool value, Endian endian)
        {
            stream.WriteValueU32((byte)(value ? 1u : 0u), endian);
        }

        public static void WriteValueB32(this Stream stream, bool value)
        {
            stream.WriteValueB32(value, Endian.Little);
        }

        public static T ReadValueEnum<T>(this Stream stream, Endian endian)
        {
            Type typeFromHandle = typeof(T);
            return (T)Enum.ToObject(typeFromHandle, EnumTypeCache.Get(typeFromHandle) switch
            {
                TypeCode.SByte => stream.ReadValueS8(),
                TypeCode.Byte => stream.ReadValueU8(),
                TypeCode.Int16 => stream.ReadValueS16(endian),
                TypeCode.UInt16 => stream.ReadValueU16(endian),
                TypeCode.Int32 => stream.ReadValueS32(endian),
                TypeCode.UInt32 => stream.ReadValueU32(endian),
                TypeCode.Int64 => stream.ReadValueS64(endian),
                TypeCode.UInt64 => stream.ReadValueU64(endian),
                _ => throw new NotSupportedException(),
            });
        }

        public static T ReadValueEnum<T>(this Stream stream)
        {
            return stream.ReadValueEnum<T>(Endian.Little);
        }

        public static void WriteValueEnum<T>(this Stream stream, object value, Endian endian)
        {
            Type typeFromHandle = typeof(T);
            switch (EnumTypeCache.Get(typeFromHandle))
            {
                case TypeCode.SByte:
                    stream.WriteValueS8((sbyte)value);
                    break;
                case TypeCode.Byte:
                    stream.WriteValueU8((byte)value);
                    break;
                case TypeCode.Int16:
                    stream.WriteValueS16((short)value, endian);
                    break;
                case TypeCode.UInt16:
                    stream.WriteValueU16((ushort)value, endian);
                    break;
                case TypeCode.Int32:
                    stream.WriteValueS32((int)value, endian);
                    break;
                case TypeCode.UInt32:
                    stream.WriteValueU32((uint)value, endian);
                    break;
                case TypeCode.Int64:
                    stream.WriteValueS64((long)value, endian);
                    break;
                case TypeCode.UInt64:
                    stream.WriteValueU64((ulong)value, endian);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public static void WriteValueEnum<T>(this Stream stream, object value)
        {
            stream.WriteValueEnum<T>(value, Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static T ReadValueEnum<T>(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueEnum<T>((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static void WriteValueEnum<T>(this Stream stream, object value, bool littleEndian)
        {
            stream.WriteValueEnum<T>(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static Guid ReadValueGuid(this Stream stream, Endian endian)
        {
            int a = stream.ReadValueS32(endian);
            short b = stream.ReadValueS16(endian);
            short c = stream.ReadValueS16(endian);
            byte[] d = stream.ReadBytes(8);
            return new Guid(a, b, c, d);
        }

        public static Guid ReadValueGuid(this Stream stream)
        {
            return stream.ReadValueGuid(Endian.Little);
        }

        public static void WriteValueGuid(this Stream stream, Guid value, Endian endian)
        {
            byte[] array = value.ToByteArray();
            stream.WriteValueS32(BitConverter.ToInt32(array, 0), endian);
            stream.WriteValueS16(BitConverter.ToInt16(array, 4), endian);
            stream.WriteValueS16(BitConverter.ToInt16(array, 6), endian);
            stream.Write(array, 8, 8);
        }

        public static void WriteValueGuid(this Stream stream, Guid value)
        {
            stream.WriteValueGuid(value, Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Guid ReadValueGuid(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueGuid((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static void WriteValueGuid(this Stream stream, Guid value, bool littleEndian)
        {
            stream.WriteValueGuid(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static float ReadValueF32(this Stream stream)
        {
            return stream.ReadValueF32(Endian.Little);
        }

        public static float ReadValueF32(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(4);
            if (ShouldSwap(endian))
            {
                return BitConverter.ToSingle(BitConverter.GetBytes(BitConverter.ToInt32(value, 0).Swap()), 0);
            }

            return BitConverter.ToSingle(value, 0);
        }

        public static void WriteValueF32(this Stream stream, float value)
        {
            stream.WriteValueF32(value, Endian.Little);
        }

        public static void WriteValueF32(this Stream stream, float value, Endian endian)
        {
            byte[] data = (ShouldSwap(endian) ? BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(value), 0).Swap()) : BitConverter.GetBytes(value));
            stream.WriteBytes(data);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static float ReadValueF32(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueF32((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static void WriteValueF32(this Stream stream, float value, bool littleEndian)
        {
            stream.WriteValueF32(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static double ReadValueF64(this Stream stream)
        {
            return stream.ReadValueF64(Endian.Little);
        }

        public static double ReadValueF64(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(8);
            if (ShouldSwap(endian))
            {
                return BitConverter.Int64BitsToDouble(BitConverter.ToInt64(value, 0).Swap());
            }

            return BitConverter.ToDouble(value, 0);
        }

        public static void WriteValueF64(this Stream stream, double value)
        {
            stream.WriteValueF64(value, Endian.Little);
        }

        public static void WriteValueF64(this Stream stream, double value, Endian endian)
        {
            byte[] data = (ShouldSwap(endian) ? BitConverter.GetBytes(BitConverter.DoubleToInt64Bits(value).Swap()) : BitConverter.GetBytes(value));
            stream.WriteBytes(data);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static double ReadValueF64(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueF64((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WriteValueF64(this Stream stream, double value, bool littleEndian)
        {
            stream.WriteValueF64(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static short ReadValueS16(this Stream stream)
        {
            return stream.ReadValueS16(Endian.Little);
        }

        public static short ReadValueS16(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(2);
            short num = BitConverter.ToInt16(value, 0);
            if (ShouldSwap(endian))
            {
                num = num.Swap();
            }

            return num;
        }

        public static void WriteValueS16(this Stream stream, short value)
        {
            stream.WriteValueS16(value, Endian.Little);
        }

        public static void WriteValueS16(this Stream stream, short value, Endian endian)
        {
            if (ShouldSwap(endian))
            {
                value = value.Swap();
            }

            byte[] bytes = BitConverter.GetBytes(value);
            stream.WriteBytes(bytes);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static short ReadValueS16(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueS16((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static void WriteValueS16(this Stream stream, short value, bool littleEndian)
        {
            stream.WriteValueS16(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static int ReadValueS32(this Stream stream)
        {
            return stream.ReadValueS32(Endian.Little);
        }

        public static int ReadValueS32(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(4);
            int num = BitConverter.ToInt32(value, 0);
            if (ShouldSwap(endian))
            {
                num = num.Swap();
            }

            return num;
        }

        public static void WriteValueS32(this Stream stream, int value)
        {
            stream.WriteValueS32(value, Endian.Little);
        }

        public static void WriteValueS32(this Stream stream, int value, Endian endian)
        {
            if (ShouldSwap(endian))
            {
                value = value.Swap();
            }

            byte[] bytes = BitConverter.GetBytes(value);
            stream.WriteBytes(bytes);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static int ReadValueS32(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueS32((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WriteValueS32(this Stream stream, int value, bool littleEndian)
        {
            stream.WriteValueS32(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static long ReadValueS64(this Stream stream)
        {
            return stream.ReadValueS64(Endian.Little);
        }

        public static long ReadValueS64(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(8);
            long num = BitConverter.ToInt64(value, 0);
            if (ShouldSwap(endian))
            {
                num = num.Swap();
            }

            return num;
        }

        public static void WriteValueS64(this Stream stream, long value)
        {
            stream.WriteValueS64(value, Endian.Little);
        }

        public static void WriteValueS64(this Stream stream, long value, Endian endian)
        {
            if (ShouldSwap(endian))
            {
                value = value.Swap();
            }

            byte[] bytes = BitConverter.GetBytes(value);
            stream.WriteBytes(bytes);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static long ReadValueS64(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueS64((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WriteValueS64(this Stream stream, long value, bool littleEndian)
        {
            stream.WriteValueS64(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static sbyte ReadValueS8(this Stream stream)
        {
            return (sbyte)stream.ReadByte();
        }

        public static void WriteValueS8(this Stream stream, sbyte value)
        {
            stream.WriteByte((byte)value);
        }

        public static byte ReadValueU8(this Stream stream)
        {
            return (byte)stream.ReadByte();
        }

        public static void WriteValueU8(this Stream stream, byte value)
        {
            stream.WriteByte(value);
        }

        public static ushort ReadValueU16(this Stream stream)
        {
            return stream.ReadValueU16(Endian.Little);
        }

        public static ushort ReadValueU16(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(2);
            ushort num = BitConverter.ToUInt16(value, 0);
            if (ShouldSwap(endian))
            {
                num = num.Swap();
            }

            return num;
        }

        public static void WriteValueU16(this Stream stream, ushort value)
        {
            stream.WriteValueU16(value, Endian.Little);
        }

        public static void WriteValueU16(this Stream stream, ushort value, Endian endian)
        {
            if (ShouldSwap(endian))
            {
                value = value.Swap();
            }

            byte[] bytes = BitConverter.GetBytes(value);
            stream.WriteBytes(bytes);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static ushort ReadValueU16(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueU16((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WriteValueU16(this Stream stream, ushort value, bool littleEndian)
        {
            stream.WriteValueU16(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static uint ReadValueU32(this Stream stream)
        {
            return stream.ReadValueU32(Endian.Little);
        }

        public static uint ReadValueU32(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(4);
            uint num = BitConverter.ToUInt32(value, 0);
            if (ShouldSwap(endian))
            {
                num = num.Swap();
            }

            return num;
        }

        public static void WriteValueU32(this Stream stream, uint value)
        {
            stream.WriteValueU32(value, Endian.Little);
        }

        public static void WriteValueU32(this Stream stream, uint value, Endian endian)
        {
            if (ShouldSwap(endian))
            {
                value = value.Swap();
            }

            byte[] bytes = BitConverter.GetBytes(value);
            stream.WriteBytes(bytes);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static uint ReadValueU32(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueU32((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static void WriteValueU32(this Stream stream, uint value, bool littleEndian)
        {
            stream.WriteValueU32(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static ulong ReadValueU64(this Stream stream)
        {
            return stream.ReadValueU64(Endian.Little);
        }

        public static ulong ReadValueU64(this Stream stream, Endian endian)
        {
            byte[] value = stream.ReadBytes(8);
            ulong num = BitConverter.ToUInt64(value, 0);
            if (ShouldSwap(endian))
            {
                num = num.Swap();
            }

            return num;
        }

        public static void WriteValueU64(this Stream stream, ulong value)
        {
            stream.WriteValueU64(value, Endian.Little);
        }

        public static void WriteValueU64(this Stream stream, ulong value, Endian endian)
        {
            if (ShouldSwap(endian))
            {
                value = value.Swap();
            }

            byte[] bytes = BitConverter.GetBytes(value);
            stream.WriteBytes(bytes);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static ulong ReadValueU64(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueU64((!littleEndian) ? Endian.Big : Endian.Little);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WriteValueU64(this Stream stream, ulong value, bool littleEndian)
        {
            stream.WriteValueU64(value, (!littleEndian) ? Endian.Big : Endian.Little);
        }

        public static string ReadString(this Stream stream, uint size)
        {
            return stream.ReadStringInternalStatic(DefaultEncoding, size, trailingNull: false);
        }

        public static string ReadString(this Stream stream, uint size, bool trailingNull)
        {
            return stream.ReadStringInternalStatic(DefaultEncoding, size, trailingNull);
        }

        public static string ReadStringZ(this Stream stream)
        {
            return stream.ReadStringInternalDynamic(DefaultEncoding, '\0');
        }

        public static void WriteString(this Stream stream, string value)
        {
            stream.WriteStringInternalStatic(DefaultEncoding, value);
        }

        public static void WriteString(this Stream stream, string value, uint size)
        {
            stream.WriteStringInternalStatic(DefaultEncoding, value, size);
        }

        public static void WriteStringZ(this Stream stream, string value)
        {
            stream.WriteStringInternalDynamic(DefaultEncoding, value, '\0');
        }

        internal static string ReadStringInternalStatic(this Stream stream, Encoding encoding, uint size, bool trailingNull)
        {
            byte[] array = new byte[size];
            stream.Read(array, 0, array.Length);
            string text = encoding.GetString(array, 0, array.Length);
            if (trailingNull)
            {
                int num = text.IndexOf('\0');
                if (num >= 0)
                {
                    text = text.Substring(0, num);
                }
            }

            return text;
        }

        internal static void WriteStringInternalStatic(this Stream stream, Encoding encoding, string value)
        {
            byte[] bytes = encoding.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }

        internal static void WriteStringInternalStatic(this Stream stream, Encoding encoding, string value, uint size)
        {
            byte[] array = encoding.GetBytes(value);
            Array.Resize(ref array, (int)size);
            stream.Write(array, 0, (int)size);
        }

        internal static string ReadStringInternalDynamic(this Stream stream, Encoding encoding, char end)
        {
            int byteCount = encoding.GetByteCount("e");
            string text = end.ToString(CultureInfo.InvariantCulture);
            int num = 0;
            byte[] array = new byte[128 * byteCount];
            while (true)
            {
                if (num + byteCount > array.Length)
                {
                    Array.Resize(ref array, array.Length + 128 * byteCount);
                }

                stream.Read(array, num, byteCount);
                if (encoding.GetString(array, num, byteCount) == text)
                {
                    break;
                }

                num += byteCount;
            }

            if (num == 0)
            {
                return "";
            }

            return encoding.GetString(array, 0, num);
        }

        internal static void WriteStringInternalDynamic(this Stream stream, Encoding encoding, string value, char end)
        {
            byte[] bytes = encoding.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
            bytes = encoding.GetBytes(end.ToString(CultureInfo.InvariantCulture));
            stream.Write(bytes, 0, bytes.Length);
        }

        public static string ReadString(this Stream stream, uint size, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, size, trailingNull: false);
        }

        public static string ReadString(this Stream stream, int size, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, (uint)size, trailingNull: false);
        }

        public static string ReadString(this Stream stream, uint size, bool trailingNull, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, size, trailingNull);
        }

        public static string ReadString(this Stream stream, int size, bool trailingNull, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, (uint)size, trailingNull);
        }

        public static string ReadStringZ(this Stream stream, Encoding encoding)
        {
            return stream.ReadStringInternalDynamic(encoding, '\0');
        }

        public static void WriteString(this Stream stream, string value, Encoding encoding)
        {
            stream.WriteStringInternalStatic(encoding, value);
        }

        public static void WriteString(this Stream stream, string value, uint size, Encoding encoding)
        {
            stream.WriteStringInternalStatic(encoding, value, size);
        }

        public static void WriteStringZ(this Stream stream, string value, Encoding encoding)
        {
            stream.WriteStringInternalDynamic(encoding, value, '\0');
        }

        public static T ReadStructure<T>(this Stream stream)
        {
            int num = Marshal.SizeOf(typeof(T));
            byte[] array = new byte[num];
            if (stream.Read(array, 0, num) != num)
            {
                throw new EndOfStreamException("could not read all of data for structure");
            }

            GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            T result = (T)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(T));
            gCHandle.Free();
            return result;
        }

        public static T ReadStructure<T>(this Stream stream, int size)
        {
            int val = Marshal.SizeOf(typeof(T));
            byte[] array = new byte[Math.Max(val, size)];
            if (stream.Read(array, 0, size) != size)
            {
                throw new EndOfStreamException("could not read all of data for structure");
            }

            GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            T result = (T)Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(T));
            gCHandle.Free();
            return result;
        }

        public static void WriteStructure<T>(this Stream stream, T structure)
        {
            int num = Marshal.SizeOf(typeof(T));
            byte[] array = new byte[num];
            GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            Marshal.StructureToPtr((object)structure, gCHandle.AddrOfPinnedObject(), fDeleteOld: false);
            gCHandle.Free();
            stream.Write(array, 0, array.Length);
        }

        public static void WriteStructure<T>(this Stream stream, T structure, int size)
        {
            int val = Marshal.SizeOf(typeof(T));
            byte[] array = new byte[Math.Max(val, size)];
            GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
            Marshal.StructureToPtr((object)structure, gCHandle.AddrOfPinnedObject(), fDeleteOld: false);
            gCHandle.Free();
            stream.Write(array, 0, array.Length);
        }
    }

    public static class Extensions
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

        public static long ReadInt40BE(this BinaryReader reader)
        {
            byte[] val = reader.ReadBytes(5);
            return val[4] | val[3] << 8 | val[2] << 16 | val[1] << 24 | val[0] << 32;
        }

        public static void Skip(this BinaryReader reader, int bytes)
        {
            reader.BaseStream.Position += bytes;
        }

        public static ushort ReadUInt16BE(this BinaryReader reader)
        {
            byte[] val = reader.ReadBytes(2);
            return (ushort)(val[1] | val[0] << 8);
        }

        public static uint ReadUInt32BE(this BinaryReader reader)
        {
            byte[] val = reader.ReadBytes(4);
            return (uint)(val[3] | val[2] << 8 | val[1] << 16 | val[0] << 24);
        }

        public static Action<T, V> GetSetter<T, V>(this FieldInfo fieldInfo)
        {
            var paramExpression = Expression.Parameter(typeof(T));
            var fieldExpression = Expression.Field(paramExpression, fieldInfo);
            var valueExpression = Expression.Parameter(fieldInfo.FieldType);
            var assignExpression = Expression.Assign(fieldExpression, valueExpression);

            return Expression.Lambda<Action<T, V>>(assignExpression, paramExpression, valueExpression).Compile();
        }

        public static Func<T, V> GetGetter<T, V>(this FieldInfo fieldInfo)
        {
            var paramExpression = Expression.Parameter(typeof(T));
            var fieldExpression = Expression.Field(paramExpression, fieldInfo);

            return Expression.Lambda<Func<T, V>>(fieldExpression, paramExpression).Compile();
        }

        public static T Read<T>(this BinaryReader reader) where T : unmanaged
        {
            byte[] result = reader.ReadBytes(Unsafe.SizeOf<T>());

            return Unsafe.ReadUnaligned<T>(ref result[0]);
        }

        public static T[] ReadArray<T>(this BinaryReader reader) where T : unmanaged
        {
            int numBytes = (int)reader.ReadInt64();

            byte[] source = reader.ReadBytes(numBytes);

            reader.BaseStream.Position += (0 - numBytes) & 0x07;

            return source.CopyTo<T>();
        }

        public static T[] ReadArray<T>(this BinaryReader reader, int size) where T : unmanaged
        {
            int numBytes = Marshal.SizeOf<T>() * size;

            byte[] source = reader.ReadBytes(numBytes);

            return source.CopyTo<T>();
        }

        public static T[] CopyTo<T>(this byte[] src) where T : unmanaged
        {
            T[] result = new T[src.Length / Marshal.SizeOf<T>()];

            if (src.Length > 0)
            {
                GCHandle gCHandle = GCHandle.Alloc(src, GCHandleType.Pinned);
                result = (T[])Marshal.PtrToStructure(gCHandle.AddrOfPinnedObject(), typeof(T[]));
                gCHandle.Free();
            }

            return result;
        }

        public static short ReadInt16BE(this BinaryReader reader)
        {
            byte[] val = reader.ReadBytes(2);
            return (short)(val[1] | val[0] << 8);
        }

        public static void CopyBytes(this Stream input, Stream output, int bytes)
        {
            byte[] buffer = new byte[0x1000];
            int read;
            while (bytes > 0 && (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        public static void CopyBytesFromPos(this Stream input, Stream output, int offset, int bytes)
        {
            byte[] buffer = new byte[0x1000];
            int read;
            int pos = 0;
            while (pos < offset && (read = input.Read(buffer, 0, Math.Min(buffer.Length, offset - pos))) > 0)
            {
                pos += read;
            }
            while (bytes > 0 && (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        public static void CopyToStream(this Stream src, Stream dst, long len)
        {
            long done = 0;

#if NET6_0_OR_GREATER
            Span<byte> buf = stackalloc byte[0x1000];
#else
            byte[] buf = new byte[0x1000];
#endif
            int count;
            do
            {
               
#if NET6_0_OR_GREATER
                count = src.Read(buf);
                dst.Write(buf.Slice(0, count));
#else
                count = src.Read(buf, 0, buf.Length);
                dst.Write(buf, 0, count);
#endif
                done += count;

            } while (count > 0);
        }

        public static void ExtractToFile(this Stream input, string path, string name)
        {
            string fullPath = Path.Combine(path, name);
            string dir = Path.GetDirectoryName(fullPath);

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (!dirInfo.Exists)
                dirInfo.Create();

            using (var fileStream = File.Open(fullPath, FileMode.Create))
            {
                input.Position = 0;
                input.CopyTo(fileStream);
            }
        }

        public static string ToHexString(this byte[] data)
        {
#if NET6_0_OR_GREATER
            return Convert.ToHexString(data);
#else
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length == 0)
                return string.Empty;
            if (data.Length > int.MaxValue / 2)
                throw new ArgumentOutOfRangeException(nameof(data), "SR.ArgumentOutOfRange_InputTooLarge");
            return HexConverter.ToString(data, HexConverter.Casing.Upper);
#endif
        }

        public static bool EqualsTo(this in MD5Hash key, byte[] array)
        {
            if (array.Length != 16)
                return false;

            ref MD5Hash other = ref Unsafe.As<byte, MD5Hash>(ref array[0]);

            if (key.lowPart != other.lowPart || key.highPart != other.highPart)
                return false;

            return true;
        }

        public static bool EqualsTo9(this in MD5Hash key, byte[] array)
        {
            if (array.Length != 16)
                return false;

            ref MD5Hash other = ref Unsafe.As<byte, MD5Hash>(ref array[0]);

            return EqualsTo9(key, other);
        }

        public static bool EqualsTo9(this in MD5Hash key, in MD5Hash other)
        {
            if (key.lowPart != other.lowPart)
                return false;

            if ((key.highPart & 0xFF) != (other.highPart & 0xFF))
                return false;

            return true;
        }

        public static bool EqualsTo(this in MD5Hash key, in MD5Hash other)
        {
            return key.lowPart == other.lowPart && key.highPart == other.highPart;
        }

        public static string ToHexString(this in MD5Hash key)
        {
            ref MD5Hash md5ref = ref Unsafe.AsRef(in key);
            var md5Span = MemoryMarshal.CreateReadOnlySpan(ref md5ref, 1);
            var span = MemoryMarshal.AsBytes(md5Span);
            return Convert.ToHexString(span);
        }

        public static MD5Hash ToMD5(this byte[] array)
        {
            if (array.Length != 16)
                throw new ArgumentException("array size != 16", nameof(array));

            return Unsafe.As<byte, MD5Hash>(ref array[0]);
        }

        public static MD5Hash ToMD5(this Span<byte> array)
        {
            if (array.Length != 16)
                throw new ArgumentException("array size != 16", nameof(array));

            return Unsafe.As<byte, MD5Hash>(ref array[0]);
        }

        public static byte[] StructToByteArray<T>(this T oStruct)
        {
            try
            {
                // This function copies the structure data into a byte[] 

                //Set the buffer to the correct size 
                byte[] buffer = new byte[Marshal.SizeOf(oStruct)];

                //Allocate the buffer to memory and pin it so that GC cannot use the 
                //space (Disable GC) 
                GCHandle h = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                // copy the struct into int byte[] mem alloc 
                Marshal.StructureToPtr(oStruct, h.AddrOfPinnedObject(), false);

                h.Free(); //Allow GC to do its job 

                return buffer; // return the byte[]. After all that's why we are here 
                               // right. 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static byte[] Serialize<T>(this T data) where T : struct
        {
            var formatter = new BinaryFormatter();
            formatter.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.TypesAlways;
            var stream = new MemoryStream();
            formatter.Serialize(stream, data);
            return stream.ToArray();
        }
        public static T Deserialize<T>(this byte[] array)
            where T : struct
        {
            var stream = new MemoryStream(array);
            var formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }
    }

    public static class CStringExtensions
    {
        /// <summary> Reads the NULL terminated string from 
        /// the current stream and advances the current position of the stream by string length + 1.
        /// <seealso cref="BinaryReader.ReadString"/>
        /// </summary>
        public static string ReadCString(this Stream reader)
        {
            return reader.ReadCString(Encoding.UTF8);
        }

        /// <summary> Reads the NULL terminated string from 
        /// the current stream and advances the current position of the stream by string length + 1.
        /// <seealso cref="BinaryReader.ReadString"/>
        /// </summary>
        public static string ReadCString(this Stream reader, Encoding encoding)
        {
            var bytes = new List<byte>();
            byte b;
            while ((b = (byte)reader.ReadByte()) != 0)
                bytes.Add(b);
            return encoding.GetString(bytes.ToArray());
        }

        public static void WriteCString(this BinaryWriter writer, string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            writer.Write(bytes);
            writer.Write((byte)0);
        }

        public static byte[] FromHexString(this string str)
        {
#if NET6_0_OR_GREATER
            return Convert.FromHexString(str);
#else
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length == 0)
                return Array.Empty<byte>();
            if ((uint)str.Length % 2 != 0)
                throw new FormatException("SR.Format_BadHexLength");

            byte[] result = new byte[str.Length >> 1];

            if (!HexConverter.TryDecodeFromUtf16(str, result))
                throw new FormatException("SR.Format_BadHexChar");

            return result;
#endif
        }
    }
}
