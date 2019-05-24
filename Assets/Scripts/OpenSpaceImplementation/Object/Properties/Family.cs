using Newtonsoft.Json;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Object.Properties {

    [Serializable]
    public class Family {
		
        public uint family_index;
        public List<State> states;
        public List<ObjectList.ObjectListReference> objectListReferences;
        public List<ObjectList> objectLists = new List<ObjectList>();
        public uint num_vector4s;
        public byte animBank;
        public byte properties;

        public string name;

        public int GetIndexOfPhysicalList(ObjectList objectList)
        {
            return objectLists.IndexOf(objectList);
        }
    }
}
