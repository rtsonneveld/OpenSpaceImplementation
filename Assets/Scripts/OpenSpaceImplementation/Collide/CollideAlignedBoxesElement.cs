using Newtonsoft.Json;
using OpenSpaceImplementation.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Collide {
    public class CollideAlignedBoxesElement : ICollideGeometricElement {
        public class IndexedAlignedBox {
            public ushort minPoint;
            public ushort maxPoint;

            public GameMaterial gameMaterial;
        }

        public CollideMeshObject mesh;
        public ushort num_boxes;
        public IndexedAlignedBox[] boxes;
		
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject("Collide Aligned Boxes");// Create object and read triangle data
                    gao.layer = LayerMask.NameToLayer("Collide");
                    CreateUnityMesh();
                }
                return gao;
            }
        }

        public CollideAlignedBoxesElement(CollideMeshObject mesh) {
            this.mesh = mesh;
        }

        private void CreateUnityMesh() {
            for (uint i = 0; i < num_boxes; i++) {
                GameObject box_gao = GameObject.CreatePrimitive(PrimitiveType.Cube);
                box_gao.layer = LayerMask.NameToLayer("Collide");
                box_gao.name = "Collide Aligned Boxes - " + i;
                box_gao.transform.SetParent(gao.transform);
                MeshFilter mf = box_gao.GetComponent<MeshFilter>();
                MeshRenderer mr = box_gao.GetComponent<MeshRenderer>();
				//MonoBehaviour.Destroy(box_gao.GetComponent<BoxCollider>());
				Vector3 center = Vector3.Lerp(mesh.vertices[boxes[i].minPoint], mesh.vertices[boxes[i].maxPoint], 0.5f);
                box_gao.transform.localPosition = center;
                box_gao.transform.localScale = mesh.vertices[boxes[i].maxPoint] - mesh.vertices[boxes[i].minPoint];

                /* TODO: visualize collision?
				mr.material = MapLoader.Loader.collideMaterial;
                if (boxes[i].gameMaterial != null && boxes[i].gameMaterial.collideMaterial != null) {
                    boxes[i].gameMaterial.collideMaterial.SetMaterial(mr);
                }
                if (mesh.type != CollideType.None) {
                    Color col = mr.material.color;
                    mr.material = MapLoader.Loader.collideTransparentMaterial;
                    mr.material.color = new Color(col.r, col.g, col.b, col.a * 0.7f);
                    switch (mesh.type) {
                        case CollideType.ZDD:
                            mr.material.SetTexture("_MainTex", Resources.Load<Texture2D>("Textures/zdd")); break;
                        case CollideType.ZDE:
                            mr.material.SetTexture("_MainTex", Resources.Load<Texture2D>("Textures/zde")); break;
                        case CollideType.ZDM:
                            mr.material.SetTexture("_MainTex", Resources.Load<Texture2D>("Textures/zdm")); break;
                        case CollideType.ZDR:
                            mr.material.SetTexture("_MainTex", Resources.Load<Texture2D>("Textures/zdr")); break;
                    }
                }*/
            }
        }

        // Call after clone
        public void Reset() {
            gao = null;
        }

        public ICollideGeometricElement Clone(CollideMeshObject mesh) {
            CollideAlignedBoxesElement sm = (CollideAlignedBoxesElement)MemberwiseClone();
            sm.mesh = mesh;
            sm.Reset();
            return sm;
        }
    }
}
