using Newtonsoft.Json;
using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.Visual;
using OpenSpaceImplementation.Visual.Deform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Object {

    [Serializable]
    public class PhysicalObject {
        
        public VisualSetLOD[] visualSet;
        public ushort visualSetType = 0;
        public CollideMeshObject collideMesh;
        public Vector3? scaleMultiplier = null;
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject("[PO]");
				}
                return gao;
            }
        }
        public DeformSet Bones {
            get {
                for (int i = 0; i < visualSet.Length; i++) {
                    if (visualSet[i].obj != null && visualSet[i].obj is MeshObject && ((MeshObject)visualSet[i].obj).bones != null) {
                        return ((MeshObject)visualSet[i].obj).bones;
                    }
                }
                return null;
            }
		}

		public PhysicalObject() {
            visualSet = new VisualSetLOD[0];
        }

        // Call after clone
        public void Reset() {
            gao = null;
        }

        public PhysicalObject Clone() {
            PhysicalObject po = (PhysicalObject)MemberwiseClone();
            po.visualSet = new VisualSetLOD[visualSet.Length];
            po.Reset();
            for (int i = 0; i < visualSet.Length; i++) {
                po.visualSet[i].LODdistance = visualSet[i].LODdistance;
                po.visualSet[i].obj = visualSet[i].obj.Clone();
                if (po.visualSet[i].obj is MeshObject) {
                    MeshObject m = ((MeshObject)po.visualSet[i].obj);
                    m.Gao.transform.parent = po.Gao.transform;
                }
            }
            if (po.visualSet.Length > 1) {
                float bestLOD = po.visualSet.Min(v => v.LODdistance);
                foreach (VisualSetLOD lod in po.visualSet) {
                    if (lod.obj.Gao != null && lod.LODdistance != bestLOD) lod.obj.Gao.SetActive(false);
                }
            }
            if (collideMesh != null) {
                po.collideMesh = collideMesh.Clone();
                po.collideMesh.Gao.transform.parent = po.Gao.transform;
            }
            return po;
        }

        public void Destroy() {
			//MapLoader.Loader.physicalObjects.Remove(this);
			if (visualSet != null) visualSet = null;
			if (collideMesh != null) collideMesh = null;
			if (gao != null) GameObject.Destroy(gao);
        }

		public void UpdateViewCollision(bool viewCollision) {
			foreach (VisualSetLOD l in visualSet) {
				if (l.obj != null) {
					GameObject gao = l.obj.Gao;
					if (gao != null) gao.SetActive(!viewCollision);
				}
			}
			if (collideMesh != null) collideMesh.SetVisualsActive(viewCollision);
		}
    }
}
