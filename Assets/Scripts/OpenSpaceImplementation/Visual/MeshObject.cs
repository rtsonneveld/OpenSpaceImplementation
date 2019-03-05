﻿using OpenSpaceImplementation.Unity;
using OpenSpaceImplementation.Visual.Deform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    /// <summary>
    /// Mesh data (both static and dynamic)
    /// </summary>
    public class MeshObject : IGeometricObject {
        
        public uint lookAtMode;
        public ushort num_vertices;
        public ushort num_subblocks;
        
        public Vector3[] vertices = null;
        public Vector3[] normals = null;
        public float[][] blendWeights = null;
        public ushort[] subblock_types = null;
		public int[][] mapping = null;
        public IGeometricElement[] subblocks = null;
        public DeformSet bones = null;
        
        private GameObject gao = null;
        public GameObject Gao {
            get {
                if (gao == null) InitGameObject();
                return gao;
            }
        }

        public void InitGameObject() {
            gao = new GameObject("MeshObject");
            gao.tag = "Visual";
            gao.layer = LayerMask.NameToLayer("Visual");
            if (bones != null) {
                GameObject child = bones.Gao;
                child.transform.SetParent(gao.transform);
                child.transform.localPosition = Vector3.zero;
            }
            for (uint i = 0; i < num_subblocks; i++) {
                if (subblocks[i] != null) {
                    GameObject child = subblocks[i].Gao;
                    child.transform.SetParent(gao.transform);
                    child.transform.localPosition = Vector3.zero;
                }
            }
            if (lookAtMode != 0) {
                BillboardBehaviour billboard = gao.AddComponent<BillboardBehaviour>();
                billboard.mode = (BillboardBehaviour.LookAtMode)lookAtMode;
            }
        }

        public void ReinitBindposes() {
            if (bones != null) {
                for (uint i = 0; i < num_subblocks; i++) {
                    if (subblocks[i] != null) {
                        if (subblocks[i] is MeshElement) {
                            ((MeshElement)subblocks[i]).ReinitBindPoses();
                        }
                    }
                }
            }
        }

        // Call after clone
        public void Reset() {
            gao = null;
        }

        public IGeometricObject Clone() {
            MeshObject m = (MeshObject)MemberwiseClone();
            m.Reset();
            m.subblocks = new IGeometricElement[num_subblocks];
            for (uint i = 0; i < m.num_subblocks; i++) {
                if (subblocks[i] != null) {
                    m.subblocks[i] = subblocks[i].Clone(m);
                    if (m.subblocks[i] is DeformSet) m.bones = (DeformSet)m.subblocks[i];
                }
            }
            return m;
        }
    }
}