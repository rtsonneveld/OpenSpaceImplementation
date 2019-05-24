using Newtonsoft.Json;
using OpenSpaceImplementation.Animation.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation {

    [Serializable]
    public class AnimationReference { // Also known as Anim3d
        public string name = null;
        public ushort num_onlyFrames;
        public byte speed;
        public byte num_channels;
        public float x;
        public float y;
        public float z;
        public ushort anim_index; // Index of animation within bank
        public byte num_events;
        public byte transition;
        public AnimMorphData[,] morphDataArray; // [channel][frame]
        public AnimA3DGeneral a3d = null;
    }
}
