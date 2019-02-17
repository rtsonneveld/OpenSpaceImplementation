using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation {

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
    }

    public class DsgVarBool : DsgVarBase {
        public static implicit operator DsgVarBool(int v)
        {
            DsgVarBool d = new DsgVarBool();
            d.value = v > 0 ? true : false;
            return d;
        }

        public static implicit operator DsgVarBool(bool v)
        {
            DsgVarBool d = new DsgVarBool();
            d.value = v;
            return d;
        }

        public static implicit operator bool(DsgVarBool d)
        {
            return (bool)d.value;
        }
    };

    public class DsgVarByte : DsgVarBase {
        public static implicit operator DsgVarByte(byte v)
        {
            DsgVarByte d = new DsgVarByte();
            d.value = (byte)v;
            return d;
        }
        public static implicit operator DsgVarByte(int v)
        {
            DsgVarByte d = new DsgVarByte();
            d.value = (byte)v;
            return d;
        }

        public static implicit operator DsgVarByte(DsgVarInt v)
        {
            DsgVarByte d = new DsgVarByte();
            d.value = (byte)v;
            return d;
        }

    };
    public class DsgVarUByte : DsgVarBase {
        public static implicit operator DsgVarUByte(byte v)
        {
            DsgVarUByte d = new DsgVarUByte();
            d.value = (byte)v;
            return d;
        }
        public static implicit operator DsgVarUByte(int v)
        {
            DsgVarUByte d = new DsgVarUByte();
            d.value = (byte)v;
            return d;
        }
    };

    public class DsgVarShort : DsgVarBase {
        public static implicit operator DsgVarShort(int v)
        {
            DsgVarShort d = new DsgVarShort();
            d.value = (short)v;
            return d;
        }
    };
    public class DsgVarUShort : DsgVarBase {
        public static implicit operator DsgVarUShort(int v)
        {
            DsgVarUShort d = new DsgVarUShort();
            d.value = (ushort)v;
            return d;
        }
    };

    public class DsgVarInt : DsgVarBase {
        public static implicit operator DsgVarInt(int v)
        {
            DsgVarInt d = new DsgVarInt();
            d.value = v;
            return d;
        }

        public static implicit operator DsgVarInt(float v)
        {
            DsgVarInt d = new DsgVarInt();
            d.value = (int)Math.Round(v);
            return d;
        }
    };

    public class DsgVarUInt : DsgVarBase {

        public static implicit operator DsgVarUInt(uint v)
        {
            DsgVarUInt d = new DsgVarUInt();
            d.value = v;
            return d;
        }

        public static implicit operator DsgVarUInt(float v)
        {
            DsgVarUInt d = new DsgVarUInt();
            d.value = (uint)Math.Round(v);
            return d;
        }
    };
    public class DsgVarFloat : DsgVarBase {

        public static implicit operator DsgVarFloat(int v)
        {
            DsgVarFloat d = new DsgVarFloat();
            d.value = (float)v;
            return d;
        }

        public static implicit operator DsgVarFloat(float v)
        {
            DsgVarFloat d = new DsgVarFloat();
            d.value = v;
            return d;
        }
    };

    public class DsgVarString : DsgVarBase { };
    public class DsgVarTextRef : DsgVarBase { };

}
