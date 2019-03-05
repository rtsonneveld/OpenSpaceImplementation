using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Sectors {
    public class Sector : MonoBehaviour {

        public List<LightInfo> staticLights;
        public VisualMaterial skyMaterial;
        public BoundingVolume sectorBorder;
        public List<Sector> Neighbours = new List<Sector>();
        public List<MeshObject> Visuals = new List<MeshObject>();
        public List<CollideMeshObject> Collision = new List<CollideMeshObject>();

        public void CreateGameObjects()
        {
            foreach(MeshObject visual in Visuals) {
                visual.InitGameObject();
                visual.Gao.transform.parent = transform.parent;
            }
        }

        //public Dictionary<string, EGeometry> Geometry;

        public bool Active = true; // TODO: sector active
    }
}
