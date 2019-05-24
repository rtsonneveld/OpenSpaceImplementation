using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimQuaternion {
        public Quaternion quaternion;

        public AnimQuaternion() {}

        public Matrix ToMatrix() {
            Matrix4x4 m = new Matrix4x4();
            float x = quaternion.x;
            float y = quaternion.y;
            float z = quaternion.z;
            float w = quaternion.w;
            float qMagnitude = 1.0f / Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x *= qMagnitude;
            y *= qMagnitude;
            z *= qMagnitude;
            w *= qMagnitude;

            float twoX = 2 * x;
            float twoY = 2 * y;
            float twoZ = 2 * z;
            float xw = twoX * w;
            float yw = twoY * w;
            float zw = twoZ * w;
            float xx = twoX * x;
            float yx = twoY * x;
            float zx = twoZ * x;
            float yy = twoY * y;
            float zy = twoZ * y;
            float zz = twoZ * z;
            m.m00 = 1.0f - (zz + yy);
            m.m01 = yx + zw;
            m.m02 = zx - yw;
            m.m10 = yx - zw;
            m.m11 = 1.0f - (zz + xx);
            m.m12 = zy + xw;
            m.m20 = zx + yw;
            m.m21 = zy - xw;
            m.m22 = 1.0f - (yy + xx);
            m.SetColumn(3, new Vector4(0, 0, 0, 1f));
            m.SetRow(3, new Vector4(0, 0, 0, 1f));
            Matrix mat = new Matrix(8, m, Vector4.one);
            return mat;
        }

        public void ConvertRotation() {
            quaternion = ToMatrix().GetRotation(convertAxes: true);
        }
    }
}
