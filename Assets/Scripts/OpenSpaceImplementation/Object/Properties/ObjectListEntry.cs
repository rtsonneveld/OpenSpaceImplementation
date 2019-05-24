using Newtonsoft.Json;
using OpenSpaceImplementation.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Object.Properties {

    [Serializable]
    public struct ObjectListEntry {
        
        public uint thirdvalue;
        public ushort unk0;
        public ushort unk1;
        public uint lastvalue;

        public Vector3? scale;
        public PhysicalObject po;
    }
}
