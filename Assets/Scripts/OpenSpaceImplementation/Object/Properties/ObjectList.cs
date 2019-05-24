using OpenSpaceImplementation.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenSpaceImplementation.Visual;

namespace OpenSpaceImplementation.Object.Properties {

    [Serializable]
    public class ObjectList {

        //public ushort num_entries;
        //public string unknownFamilyName;

        public ObjectListEntry[] entries;

        public int Count
        {
            get
            {
                return entries.Count();
            }
        }

        public class ObjectListReference {
            public string Hash;
        }
    }
}
