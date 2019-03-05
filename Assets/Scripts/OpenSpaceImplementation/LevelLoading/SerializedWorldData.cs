using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.Visual;
using OpenSpaceImplementation.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.LevelLoading {
    public class SerializedWorldData {

        public struct ESector {
            public Visual.VisualMaterial SkyMaterial;
            public List<String> Neighbours;
            public Dictionary<string, EGeometry> Geometry;
            public BoundingVolume SectorBorder;
            //public List<LightInfo> LightInfo;
            public bool Virtual;
        }

        public struct EGeometry { // IPO
            public MeshObject Visuals;
            public CollideMeshObject Collision;
        }

        public Dictionary<string, ESector> Sectors;
    }
}
