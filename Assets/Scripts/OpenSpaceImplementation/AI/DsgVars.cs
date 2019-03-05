using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSpaceImplementation.Strings;

namespace OpenSpaceImplementation.AI {

    public class DsgVarBase {
        public object value;

        public static implicit operator float(DsgVarBase d)
        {
            return (float)d.value;
        }

        public static implicit operator int(DsgVarBase d)
        {
            return (int)d.value;
        }

        public static implicit operator byte(DsgVarBase d)
        {
            return (byte)d.value;
        }

        public static implicit operator short(DsgVarBase d)
        {
            return (short)d.value;
        }

        public static T CreateDsgVar<T>(object value) where T:DsgVarBase, new()
        {
            T dsgVar = new T();
            dsgVar.value = value;
            return dsgVar;
        }
    }

    public class DsgVarBool : DsgVarBase {
        public static implicit operator DsgVarBool(int v)
        {
            return CreateDsgVar<DsgVarBool>(v > 0 ? true : false);
        }

        public static implicit operator DsgVarBool(bool v)
        {
            return CreateDsgVar<DsgVarBool>(v);
        }

        public static implicit operator bool(DsgVarBool d)
        {
            return (bool)d.value;
        }
    };

    public class DsgVarByte : DsgVarBase {
        public static implicit operator DsgVarByte(byte v)
        {
            return CreateDsgVar<DsgVarByte>((byte)v);
        }
        public static implicit operator DsgVarByte(int v)
        {
            return CreateDsgVar<DsgVarByte>((byte)v);
        }
        public static implicit operator DsgVarByte(float v)
        {
            return CreateDsgVar<DsgVarByte>((byte)v);
        }

        public static implicit operator DsgVarByte(DsgVarInt v)
        {
            return CreateDsgVar<DsgVarByte>((byte)v);
        }

    };
    public class DsgVarUByte : DsgVarBase {
        public static implicit operator DsgVarUByte(byte v)
        {
            return CreateDsgVar<DsgVarUByte>((byte)v);
        }
        public static implicit operator DsgVarUByte(int v)
        {
            return CreateDsgVar<DsgVarUByte>((byte)v);
        }
    };

    public class DsgVarShort : DsgVarBase {
        public static implicit operator DsgVarShort(int v)
        {
            return CreateDsgVar<DsgVarShort>((short)v);
        }
    };
    public class DsgVarUShort : DsgVarBase {
        public static implicit operator DsgVarUShort(int v)
        {
            return CreateDsgVar<DsgVarUShort>((ushort)v);
        }
    };

    public class DsgVarInt : DsgVarBase {
        public static implicit operator DsgVarInt(int v)
        {
            return CreateDsgVar<DsgVarInt>(v);
        }

        public static implicit operator DsgVarInt(float v)
        {
            return CreateDsgVar<DsgVarInt>((int)Math.Round(v));
        }

        public static implicit operator bool(DsgVarInt d)
        {
            return ((int)d.value != 0) ? true : false;
        }
    };

    public class DsgVarUInt : DsgVarBase {

        public static implicit operator DsgVarUInt(uint v)
        {
            return CreateDsgVar<DsgVarUInt>(v);
        }

        public static implicit operator DsgVarUInt(float v)
        {
            return CreateDsgVar<DsgVarUInt>((uint)Math.Round(v));
        }
    };
    public class DsgVarFloat : DsgVarBase {

        public static implicit operator DsgVarFloat(int v)
        {
            return CreateDsgVar<DsgVarFloat>((float)v);
        }

        public static implicit operator DsgVarFloat(float v)
        {
            return CreateDsgVar<DsgVarFloat>(v);
        }

        public static implicit operator bool(DsgVarFloat d)
        {
            return ((float)d.value != 0) ? true : false;
        }
    };

    public class DsgVarString : DsgVarBase {
        public static implicit operator DsgVarString(string str)
        {
            return CreateDsgVar<DsgVarString>(str);
        }
    };
    public class DsgVarTextRef : DsgVarBase { };

    public class DsgVarList {
        // TODO: stub
    }

}
