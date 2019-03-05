using System;
using UnityEngine;

namespace OpenSpaceImplementation.Collide {
    public class BoundingVolume {

        public enum Type {
            Sphere, Box
        }

        public Type type;

        // For Sphere
        public Vector3 sphereCenter;
        public float sphereRadius;

        // For Box
        public Vector3 boxMin;
        public Vector3 boxMax;

        public Vector3 boxCenter; // calculated from boxMin, boxMax
        public Vector3 boxSize; // calculated from boxMin, boxMax

        public Vector3 Center {
            get {
                switch (type) {
                    case Type.Box:
                        return boxCenter;
                    case Type.Sphere:
                        return sphereCenter;
                    default:
                        return Vector3.zero;
                }
            }
        }

        public Vector3 Size {
            get {
                switch (type) {
                    case Type.Box:
                        return boxSize;
                    case Type.Sphere:
                        return Vector3.one * sphereRadius * 0.5f;
                    default:
                        return Vector3.zero;
                }
            }
        }

        public bool ContainsPoint(Vector3 pos) {
            switch (type) {
                case Type.Box:
                    return pos.x >= boxMin.x && pos.x <= boxMax.x
                    && pos.y >= boxMin.y && pos.y <= boxMax.y
                    && pos.z >= boxMin.z && pos.z <= boxMax.z;
                case Type.Sphere:
                    return Vector3.Distance(pos, sphereCenter) <= sphereRadius;
                default:
                    throw new ArgumentException("Type should be Box or Sphere");
            }
        }
    }
}