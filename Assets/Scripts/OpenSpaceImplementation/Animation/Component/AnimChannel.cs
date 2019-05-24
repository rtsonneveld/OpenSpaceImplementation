using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimChannel {
        public ushort unk0;
        public short id;
        public ushort vector;
        public ushort numOfNTTO;
        public uint framesKF;
        public uint keyframe;

    }
}
