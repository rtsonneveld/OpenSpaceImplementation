using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.LevelLoading {
    public class SerializedPersoData {

        public Dictionary<string, EPerso> Persos;

        public struct EPerso {
            public string ParentPerso;
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Scale;
            public string Family;
            public string AIModel;
            public Dictionary<string, EDsgVar> Variables;
            public EStandardGame StandardGame;
            //public StandardGame StandardGame;
        }

        public struct EStandardGame {
            public uint CustomBits;
            public bool IsAlwaysActive;
            public bool IsMainActor;
            public bool IsAPlatform;
            public byte UpdateCheckByte;
            public byte TransparencyZoneMin;
            public byte TransparencyZoneMax;
            public float TooFarLimit;
        }

        public struct EDsgVar {
            public int type; // DsgVarInfoEntry.DsgVarType
            public object value;
        }
    }
}
