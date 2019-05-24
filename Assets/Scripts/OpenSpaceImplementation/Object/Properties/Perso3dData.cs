using UnityEngine;
using UnityEditor;
using System;

namespace OpenSpaceImplementation.Object.Properties {

    [Serializable]
    public class Perso3dData {

        public Family Family = null;
        public ObjectList ObjectList = null;
        public State StateCurrent = null;
    }
}