using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimKeyframe {
        public float x;
        public float y;
        public float z;
        public float positionMultiplier = 1f;
        public ushort frame;
        public ushort flags;
        public ushort quaternion;
        public ushort quaternion2;
        public ushort scaleVector;
        public ushort positionVector;
        public double interpolationFactor;

        public static ushort flag_endKF = (1 << 7);

        public bool IsEndKeyframe {
            get {
                if((flags & flag_endKF) != 0) return true;
                return false;
            }
        }
    }
}
