using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Collide {
    /// <summary>
    /// Mesh data (both static and dynamic)
    /// </summary>
    public class CollideMeshObject {
        //public PhysicalObject po;
        public CollideType type;

        public GameObject gao = null;
		
        public ushort num_vertices;
        public ushort num_subblocks;

        public Vector3[] vertices = null;
        public Vector3[] normals = null;
        public ushort[] subblock_types = null;
        public ICollideGeometricElement[] subblocks = null;

        public CollideMeshObject(CollideType type = CollideType.None) {
            this.type = type;
        }

        public void SetVisualsActive(bool active) {
			if (gao == null) return;
            Renderer[] renderers = gao.GetComponentsInChildren<Renderer>(includeInactive: true);
            foreach (Renderer ren in renderers) {
                ren.enabled = active;
            }
            /*if (subblocks != null) {
                foreach (ICollideGeometricElement subblock in subblocks) {
                    GameObject child = subblock.Gao;
                    if (child != null) {
                        Renderer mainRen = child.GetComponent<Renderer>();
                    }
                    //subblock.Gao
                }
            }*/
        }

        public CollideMeshObject Clone() {
            CollideMeshObject m = (CollideMeshObject)MemberwiseClone();
            m.gao = new GameObject("Collide Set");
            m.gao.tag = "Collide";
            m.subblocks = new ICollideGeometricElement[num_subblocks];
            for (uint i = 0; i < m.num_subblocks; i++) {
                if (subblocks[i] != null) {
                    m.subblocks[i] = subblocks[i].Clone(m);
                }
            }
            for (uint i = 0; i < m.num_subblocks; i++) {
                if (m.subblocks[i] != null) {
                    GameObject child = m.subblocks[i].Gao;
                    child.transform.SetParent(m.gao.transform);
                    child.transform.localPosition = Vector3.zero;
                    /*if (m.subblocks[i] is CollideMeshElement) {
                        GameObject child = ((CollideMeshElement)m.subblocks[i]).Gao;
                        child.transform.SetParent(m.gao.transform);
                        child.transform.localPosition = Vector3.zero;
                    } else if (m.subblocks[i] is CollideSpheresElement) {
                        GameObject child = ((CollideSpheresElement)m.subblocks[i]).Gao;
                        child.transform.SetParent(m.gao.transform);
                        child.transform.localPosition = Vector3.zero;
                    } else if (m.subblocks[i] is CollideAlignedBoxesElement) {
                        GameObject child = ((CollideAlignedBoxesElement)m.subblocks[i]).Gao;
                        child.transform.SetParent(m.gao.transform);
                        child.transform.localPosition = Vector3.zero;
                    }*/
                }
            }
            m.SetVisualsActive(false); // Invisible by default
            //m.gao.SetActive(false); // Invisible by default
            return m;
        }
    }
}
