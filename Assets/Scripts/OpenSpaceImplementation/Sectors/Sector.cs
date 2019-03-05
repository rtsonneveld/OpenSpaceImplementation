using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Sectors {
    public class Sector {

        public GameObject Gao; // TODO: sector gameobjects

        public List<LightInfo> staticLights;
        public VisualMaterial skyMaterial;
        public BoundingVolume sectorBorder;

        public bool Active = true; // TODO: sector active
    }
}
