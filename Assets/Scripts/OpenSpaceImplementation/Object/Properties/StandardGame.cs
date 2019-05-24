using UnityEngine;
using UnityEditor;

namespace OpenSpaceImplementation.Object.Properties {
    public class StandardGame {
        public uint[] objectTypes = new uint[3];

        public uint customBits;
        public uint aiCustomBits;
        public byte isAPlatform;
        public byte updateCheckByte;
        public byte transparencyZoneMin;
        public byte transparencyZoneMax;
        public uint customBitsInitial;
        public uint aiCustomBitsInitial;
        public float tooFarLimit;

        public bool IsAlwaysActive
        {
            get
            {
                return ((updateCheckByte >> 6) & 1) != 0;
            }
        }

        public bool IsMainActor
        {
            get
            {
                return (customBits & 0x80000000) == 0x80000000;
            }
        }

        public bool ConsideredOnScreen()
        {
            return (updateCheckByte & (1 << 5)) != 0;
        }

        public bool ConsideredTooFarAway()
        {
            return (updateCheckByte & (1 << 7)) != 0;
        }
        
    }
}