using Newtonsoft.Json;
using OpenSpaceImplementation.General;
using OpenSpaceImplementation.Sectors;
using OpenSpaceImplementation.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    public class LightInfo : IEquatable<LightInfo> {

        [JsonIgnore]
        public List<Sector> containingSectors;

        public string offsetString;

        public byte turnedOn;
        public byte castShadows;
        public byte giroPhare;
        public byte pulse;
        // ...
        public ushort type;
        // ...
        public float far;
        public float near;
        public float littleAlpha_fogInfinite;
        public float bigAlpha_fogBlendNear;
        public float giroStep;
        public float pulseStep;
        public float pulseMaxRange;
        public float giroAngle;
        // ...
        public Matrix transMatrix;
        // ...
        public Vector4 color;
        public float shadowIntensity;
        // ...
        public byte sendLightFlag;
        public byte objectLightedFlag;
        public byte paintingLightFlag;
        public byte alphaLightFlag;
        public Vector3 interMinPos;
        public Vector3 exterMinPos;
        public Vector3 interMaxPos;
        public Vector3 exterMaxPos;
        public Vector3 exterCenterPos;
        // ...
        public float attFactor3;
        public float intensityMin_fogBlendFar;
        public float intensityMax;
        public Vector4 background_color;
        public uint createsShadowsOrNot;

        public void OffsetAll(Vector3 offset)
        {
            transMatrix.SetTRS(transMatrix.GetPosition() + offset, transMatrix.GetRotation(), transMatrix.GetScale());
        }

        public string name = null;

        [Flags]
        public enum ObjectLightedFlag {
            None = 0,
            Environment = 1,
            Perso = 2
        }

        public enum LightType {
            Unknown = 0,
            Parallel = 1,
            Spherical = 2,
            Hotspot = 3, // R2: Cone
            Ambient = 4,
            ParallelOtherType = 5, // also seems to be the one with exterMinPos & exterMaxPos, so not spherical
            Fog = 6, // Also background color
            ParallelInASphere = 7,
            SphereOtherType = 8 // ignores persos?
        }

        private LightBehaviour light;
        public LightBehaviour Light
        {
            get
            {
                return light;
            }
        }

        public LightInfo()
        {
            containingSectors = new List<Sector>();
        }

        public void CreateGameObject()
        {
            if (light == null) {
                GameObject gao = new GameObject((name == null ? "Light" : "Light " + name) +
                    "Type: " + type + " - Far: " + far + " - Near: " + near +
                    //" - FogBlendNear: " + bigAlpha_fogBlendNear + " - FogBlendFar: " + intensityMin_fogBlendFar +
                    " - AlphaLightFlag: " + alphaLightFlag +
                    " - PaintingLightFlag: " + paintingLightFlag +
                    " - ObjectLightedFlag: " + objectLightedFlag);
                Vector3 pos = transMatrix.GetPosition(convertAxes: true);
                Quaternion rot = transMatrix.GetRotation(convertAxes: true) * Quaternion.Euler(-90, 0, 0);
                Vector3 scale = transMatrix.GetScale(convertAxes: true);
                gao.transform.localPosition = pos;
                gao.transform.localRotation = rot;
                gao.transform.localScale = scale;
                light = gao.AddComponent<LightBehaviour>();
                light.li = this;
            }
        }

        public override bool Equals(System.Object obj)
        {
            return obj is LightInfo && this == (LightInfo)obj;
        }

        public bool Equals(LightInfo other)
        {
            return this == (LightInfo)other;
        }

        public bool IsObjectLighted(ObjectLightedFlag flags)
        {
            if (Controller.Settings.engineVersion == Settings.EngineVersion.Montreal) return true;
            if (flags == ObjectLightedFlag.Environment) return true;
            return ((objectLightedFlag & (int)flags) == (int)flags);
        }
    }
}
