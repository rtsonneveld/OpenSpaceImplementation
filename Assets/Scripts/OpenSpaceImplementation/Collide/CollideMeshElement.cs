using Newtonsoft.Json;
using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Collide {

    [Serializable]
    public class CollideMeshElement : ICollideGeometricElement {

        public CollideMeshObject Mesh { get; set; }
        public ushort num_triangles;
        public ushort num_mapping;
        public ushort num_mapping_entries;

        public GameMaterial gameMaterial;
        public int[] triangles = null;
        public Vector3[] normals = null;
        public int[] mapping = null;
        public Vector2[] uvs = null;
		
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject("Collide Submesh");// Create object and read triangle data
                    gao.layer = LayerMask.NameToLayer("Collide");
                    CreateUnityMesh();
                }
                return gao;
            }
        }

        private void CreateUnityMesh() {
            if(num_triangles > 0) {
                Vector3[] new_vertices = new Vector3[num_triangles * 3];
                Vector3[] new_normals = new Vector3[num_triangles * 3];
                Vector2[] new_uvs = new Vector2[num_triangles * 3];

                for (int j = 0; j < num_triangles * 3; j++) {
                    new_vertices[j] = Mesh.vertices[triangles[j]];
                    if(normals != null) new_normals[j] = normals[j/3];
                    if (uvs != null) new_uvs[j] = uvs[mapping[j]];
                }
                int[] new_triangles = new int[num_triangles * 3];
                for (int j = 0; j < num_triangles; j++) {
                    new_triangles[(j * 3) + 0] = (j * 3) + 0;
                    new_triangles[(j * 3) + 1] = (j * 3) + 2;
                    new_triangles[(j * 3) + 2] = (j * 3) + 1;
                }
                Mesh meshUnity = new Mesh();
                meshUnity.vertices = new_vertices;
                if(normals != null) meshUnity.normals = new_normals;
                meshUnity.triangles = new_triangles;
                if (uvs != null) meshUnity.uv = new_uvs;
				if (normals == null) meshUnity.RecalculateNormals();
                MeshFilter mf = gao.AddComponent<MeshFilter>();
                mf.mesh = meshUnity;
                MeshRenderer mr = gao.AddComponent<MeshRenderer>();
                MeshCollider mc = gao.AddComponent<MeshCollider>();
                mc.sharedMesh = mf.sharedMesh;

                /*mr.material = MapLoader.Loader.collideMaterial; TODO visualize collision?
                if (gameMaterial != null && gameMaterial.collideMaterial != null) {
                    gameMaterial.collideMaterial.SetMaterial(mr);
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
                }
                */
            }
        }


        // Call after clone
        public void Reset() {
            gao = null;
        }

        public ICollideGeometricElement Clone(CollideMeshObject mesh) {
            CollideMeshElement sm = (CollideMeshElement)MemberwiseClone();
            sm.Mesh = mesh;
            sm.Reset();
            return sm;
        }
    }
}
