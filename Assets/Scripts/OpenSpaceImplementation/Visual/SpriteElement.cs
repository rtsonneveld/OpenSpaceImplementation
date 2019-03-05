using Newtonsoft.Json;
using OpenSpaceImplementation.General;
using OpenSpaceImplementation.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    public class IndexedSprite {
        public Vector2 size;
        public Vector3 constraint;
        public Vector2 uv1;
        public Vector2 uv2;
        public ushort centerPoint;

        public Vector2 info_scale;
        public Vector2 info_unknown;
        public GameMaterial gameMaterial;
        public VisualMaterial visualMaterial = null;
        [JsonIgnore] public Mesh meshUnity = null;
    }

    public class SpriteElement : IGeometricElement {
        public MeshObject mesh;

        public ushort num_sprites;
        public IndexedSprite[] sprites;
		
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject();// Create object and read triangle data
                    gao.layer = LayerMask.NameToLayer("Visual");
                    BillboardBehaviour billboard = gao.AddComponent<BillboardBehaviour>();
                    billboard.mode = BillboardBehaviour.LookAtMode.ViewRotation;
                    CreateUnityMesh();
                }
                return gao;
            }
        }

        public SpriteElement(MeshObject mesh) {
            this.mesh = mesh;
        }

        private void CreateUnityMesh() {
            for (uint i = 0; i < num_sprites; i++) {
                bool mirrorX = false;
                bool mirrorY = false;
                GameObject spr_gao = new GameObject("SpriteElement" + i);
                spr_gao.transform.SetParent(gao.transform);
                MeshFilter mf = spr_gao.AddComponent<MeshFilter>();
                MeshRenderer mr = spr_gao.AddComponent<MeshRenderer>();
                BoxCollider bc = spr_gao.AddComponent<BoxCollider>();
                bc.size = new Vector3(0, sprites[i].info_scale.y * 2, sprites[i].info_scale.x * 2);
                spr_gao.layer = LayerMask.NameToLayer("Visual");
				if (sprites[i].visualMaterial != null) {
					if (Controller.ControllerInstance.settings.game != Settings.Game.R2Revolution &&
						sprites[i].visualMaterial.textures != null &&
						sprites[i].visualMaterial.textures.Count > 0) {
						TextureInfo mainTex = sprites[i].visualMaterial.textures[0].texture;

						if (mainTex != null && mainTex.IsMirrorX) mirrorX = true;
						if (mainTex != null && mainTex.IsMirrorY) mirrorY = true;
					}
					//Material unityMat = sprites[i].visualMaterial.MaterialBillboard;
					Material unityMat = sprites[i].visualMaterial.GetMaterial(VisualMaterial.Hint.Billboard);
					bool receiveShadows = (sprites[i].visualMaterial.properties & VisualMaterial.property_receiveShadows) != 0;
					//if (num_uvMaps > 1) unityMat.SetFloat("_UVSec", 50f);
					//if (r3mat.Material.GetColor("_EmissionColor") != Color.black) print("Mesh with emission: " + name);
					mr.sharedMaterial = unityMat;
					/*mr.material.SetFloat("_ScaleX", sprites[i].info_scale.x);
                    mr.material.SetFloat("_ScaleY", sprites[i].info_scale.y);*/
					if (!receiveShadows) mr.receiveShadows = false;
					if (sprites[i].visualMaterial.animTextures.Count > 0) {
						MultiTextureMaterial mtmat = mr.gameObject.AddComponent<MultiTextureMaterial>();
						mtmat.visMat = sprites[i].visualMaterial;
						mtmat.mat = mr.sharedMaterial;
					}
				} else {
					Material transMat = new Material(Controller.VisualMaterialManager.baseTransparentMaterial);
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, new Color(0, 0, 0, 0));
					transMat.SetTexture("_Tex0", tex);
					mr.sharedMaterial = transMat;
				}
				if (sprites[i].meshUnity == null) {
					sprites[i].meshUnity = new Mesh();
					Vector3[] vertices = new Vector3[4];
					vertices[0] = new Vector3(0, -sprites[i].info_scale.y, -sprites[i].info_scale.x);
					vertices[1] = new Vector3(0, -sprites[i].info_scale.y, sprites[i].info_scale.x);
					vertices[2] = new Vector3(0, sprites[i].info_scale.y, -sprites[i].info_scale.x);
					vertices[3] = new Vector3(0, sprites[i].info_scale.y, sprites[i].info_scale.x);
					Vector3[] normals = new Vector3[4];
					normals[0] = Vector3.forward;
					normals[1] = Vector3.forward;
					normals[2] = Vector3.forward;
					normals[3] = Vector3.forward;
					Vector3[] uvs = new Vector3[4];
					uvs[0] = new Vector3(0, 0 - (mirrorY ? 1 : 0), 1);
					uvs[1] = new Vector3(1 + (mirrorX ? 1 : 0), 0 - (mirrorY ? 1 : 0), 1);
					uvs[2] = new Vector3(0, 1, 1);
					uvs[3] = new Vector3(1 + (mirrorX ? 1 : 0), 1, 1);
					int[] triangles = new int[] { 0, 2, 1, 1, 2, 3 };

					sprites[i].meshUnity.vertices = vertices;
					sprites[i].meshUnity.normals = normals;
					sprites[i].meshUnity.triangles = triangles;
					sprites[i].meshUnity.SetUVs(0, uvs.ToList());
				}

                
                mf.sharedMesh = sprites[i].meshUnity;
            }
        }

        // Call after clone
        public void Reset() {
            gao = null;
        }

        public IGeometricElement Clone(MeshObject mesh) {
            SpriteElement sm = (SpriteElement)MemberwiseClone();
            sm.mesh = mesh;
            sm.Reset();
            return sm;
        }
    }
}
