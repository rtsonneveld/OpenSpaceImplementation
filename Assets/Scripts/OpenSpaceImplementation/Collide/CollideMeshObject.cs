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
    
    [Serializable]
    public class CollideMeshObject {
        //public PhysicalObject po;
        public CollideType type;

        public GameObject Gao { get; set; }
		
        public ushort num_vertices;
        public ushort num_subblocks;

        public Vector3[] vertices = null;
        public Vector3[] normals = null;
        public ushort[] subblock_types = null;
        public ICollideGeometricElement[] subblocks = null;

        public CollideMeshObject(CollideType type = CollideType.None) {
            this.type = type;
        }

        public Vector3 AveragePosition
        {
            get
            {
                Vector3 total = new Vector3(0, 0, 0);
                float c = 0;

                foreach (Vector3 v in vertices) {
                    total += v;
                    c++;
                }

                return c == 0 ? Vector3.zero : total / c;
            }
        }

        public void OffsetAll(Vector3 offset)
        {
            for(int i=0;i<vertices.Length;i++) {
                vertices[i] = vertices[i] + offset;
            }
        }

        public void SetParentReferenceForSubblocks()
        {
            foreach (ICollideGeometricElement e in subblocks) {
                e.Mesh = this;
            }
        }

        public void InitGameObject()
        {
            SetParentReferenceForSubblocks();

            Gao = new GameObject("Collide Set " + (type != CollideType.None ? type + " " : ""));
            Gao.tag = "Collide";
            Gao.layer = LayerMask.NameToLayer("Collide");

            for (uint i = 0; i < num_subblocks; i++) {
                if (subblocks[i] != null) {
                    GameObject child = subblocks[i].Gao;
                    child.transform.SetParent(Gao.transform);
                    child.transform.localPosition = Vector3.zero;
                }
            }
            SetVisualsActive(false); // Invisible by default
        }


        public void SetVisualsActive(bool active) {
			if (Gao == null) return;
            Renderer[] renderers = Gao.GetComponentsInChildren<Renderer>(includeInactive: true);
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
            m.Gao = new GameObject("Collide Set");
            m.Gao.tag = "Collide";
            m.subblocks = new ICollideGeometricElement[num_subblocks];
            for (uint i = 0; i < m.num_subblocks; i++) {
                if (subblocks[i] != null) {
                    m.subblocks[i] = subblocks[i].Clone(m);
                }
            }
            for (uint i = 0; i < m.num_subblocks; i++) {
                if (m.subblocks[i] != null) {
                    GameObject child = m.subblocks[i].Gao;
                    child.transform.SetParent(m.Gao.transform);
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
