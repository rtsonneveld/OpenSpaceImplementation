using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual {

    [Serializable]
    public class VisualSetLOD {
        public float LODdistance;
        public IGeometricObject obj;
    }
}
