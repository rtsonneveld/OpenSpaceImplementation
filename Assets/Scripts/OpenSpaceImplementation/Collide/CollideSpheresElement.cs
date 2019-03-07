using Newtonsoft.Json;
using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Collide {
    public class CollideSpheresElement : ICollideGeometricElement {
        public class IndexedSphere {
            public float radius;
            public ushort centerPoint;

            public GameMaterial gameMaterial;
        }

        public CollideMeshObject Mesh { get; set; }
        public ushort num_spheres;
        public IndexedSphere[] spheres;
		
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject("Collide Spheres");// Create object and read triangle data
                    gao.layer = LayerMask.NameToLayer("Collide");
                    CreateUnityMesh();
                }
                return gao;
            }
        }

        private void CreateUnityMesh() {
            for (uint i = 0; i < num_spheres; i++) {
                GameObject sphere_gao = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere_gao.name = "Collide Spheres - " + i;
                sphere_gao.transform.SetParent(gao.transform);
                MeshFilter mf = sphere_gao.GetComponent<MeshFilter>();
                MeshRenderer mr = sphere_gao.GetComponent<MeshRenderer>();
                //MonoBehaviour.Destroy(sphere_gao.GetComponent<SphereCollider>());
                sphere_gao.transform.localPosition = Mesh.vertices[spheres[i].centerPoint];
                sphere_gao.transform.localScale = Vector3.one * spheres[i].radius * 2; // default Unity sphere radius is 0.5
                sphere_gao.layer = LayerMask.NameToLayer("Collide");

                /* TODO: visualize collision?
                mr.material = MapLoader.Loader.collideMaterial;
                if (spheres[i].gameMaterial != null && spheres[i].gameMaterial.collideMaterial != null) {
                    spheres[i].gameMaterial.collideMaterial.SetMaterial(mr);
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
            CollideSpheresElement sm = (CollideSpheresElement)MemberwiseClone();
            sm.Mesh = mesh;
            sm.Reset();
            return sm;
        }
    }
}
