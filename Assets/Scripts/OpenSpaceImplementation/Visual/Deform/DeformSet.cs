using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual.Deform {
    public class DeformSet : IGeometricElement {
        public MeshObject mesh;

        public ushort num_weights;
        public byte num_bones;
        
        public DeformVertexWeights[] r3weights;
        public DeformBone[] r3bones;

        public BoneWeight[] weights;
        public Transform[] bones;
        public Matrix4x4[] bindPoses;
		
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject("Deform Set");
                    InitUnityBones();
                }
                return gao;
            }
        }

        public DeformSet(MeshObject mesh) {
            this.mesh = mesh;
        }

        public void InitUnityBones() {
            weights = new BoneWeight[mesh.num_vertices];
            for (int i = 0; i < mesh.num_vertices; i++) {
                weights[i] = new BoneWeight();
                weights[i].boneIndex0 = 0;
                weights[i].boneIndex1 = 0;
                weights[i].boneIndex2 = 0;
                weights[i].boneIndex3 = 0;
                weights[i].weight0 = 1f;
                weights[i].weight1 = 0;
                weights[i].weight2 = 0;
                weights[i].weight3 = 0;
            }
            for (int i = 0; i < num_weights; i++) {
                weights[r3weights[i].vertexIndex] = r3weights[i].UnityWeight;
            }
            bones = new Transform[num_bones];
            for (int i = 0; i < num_bones; i++) {
                bones[i] = r3bones[i].UnityBone;
            }
            bindPoses = new Matrix4x4[num_bones];
            for (int i = 0; i < num_bones; i++) {
                bindPoses[i] = bones[i].worldToLocalMatrix * Gao.transform.localToWorldMatrix;
            }
            for (int j = 0; j < num_bones; j++) {
                Transform b = bones[j];
                b.transform.SetParent(gao.transform);
            }
        }

        public void RecalculateBindPoses() {
            bindPoses = new Matrix4x4[num_bones];
            for (int i = 0; i < num_bones; i++) {
                bindPoses[i] = bones[i].worldToLocalMatrix * Gao.transform.localToWorldMatrix;
            }
            mesh.ReinitBindposes();
        }
        

        // Call after clone
        public void Reset() {
            gao = null;
        }

        public IGeometricElement Clone(MeshObject mesh) {
            DeformSet d = (DeformSet)MemberwiseClone();
            d.Reset();
            d.mesh = mesh;
            d.r3bones = new DeformBone[r3bones.Length];
            for (int i = 0; i < r3bones.Length; i++) {
                d.r3bones[i] = r3bones[i].Clone();
            }
            d.r3weights = new DeformVertexWeights[r3weights.Length];
            for (int i = 0; i < r3weights.Length; i++) {
                d.r3weights[i] = r3weights[i].Clone();
            }
            return d;
        }
    }
}
