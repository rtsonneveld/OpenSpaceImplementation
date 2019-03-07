using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.Unity;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Sectors {
    public class Sector : MonoBehaviour {

        public VisualMaterial skyMaterial;
        public BoundingVolume sectorBorder;
        public List<Sector> Neighbours = new List<Sector>();
        public List<MeshObject> Visuals = new List<MeshObject>();
        public List<CollideMeshObject> Collision = new List<CollideMeshObject>();
        public List<LightInfo> Lights = new List<LightInfo>();

        public void CreateGameObjects()
        {
            // Remove null entries
            Visuals.RemoveAll((v) => v == null);
            Collision.RemoveAll((v) => v == null);
            
            // Calculate average position and translate to fix sector pivots
            List<Vector3> averagePositionList = Visuals.Select(v => v.AveragePosition).Concat(Collision.Select(c => c.AveragePosition)).ToList();
            Vector3 total = Vector3.zero;
            foreach (Vector3 v in averagePositionList) {
                total += v;
            }

            Vector3 averagePosition = averagePositionList.Count > 0 ? (total / (float)averagePositionList.Count) : Vector3.zero;
            Visuals.ForEach(v => v.OffsetAll(-averagePosition));
            Collision.ForEach(c => c.OffsetAll(-averagePosition));

            transform.localPosition += averagePosition;

            foreach (MeshObject visual in Visuals) {
                visual.InitGameObject();
                visual.Gao.transform.parent = transform;
                visual.Gao.transform.localPosition = Vector3.zero;
            }

            foreach (CollideMeshObject collision in Collision) {
                collision.InitGameObject();
                collision.Gao.transform.parent = transform;
                collision.Gao.transform.localPosition = Vector3.zero;
            }
        }

        //public Dictionary<string, EGeometry> Geometry;

        public bool Active = true; // TODO: sector active
    }
}
