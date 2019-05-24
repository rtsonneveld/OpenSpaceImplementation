using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimA3DGeneral {

        public ushort speed;
        public ushort num_vectors;
        public ushort num_quaternions;
        public ushort num_hierarchies;
        public ushort num_NTTO;
        public ushort num_numNTTO;
        public ushort num_deformations;
        public ushort num_channels;
        public ushort num_onlyFrames;
        public ushort unk_12;
        public ushort unk_14;
        public ushort num_keyframes;
        public ushort num_events;
        public ushort unk_1A;
        public ushort subtractFramesForSpeed;
        public ushort unk_1E;
        public ushort speed2;
        public ushort unk_22_morphs;
        public ushort start_vectors2;
        public ushort start_quaternions2;
        public ushort num_morphData;
        public ushort start_vectors;
        public ushort start_quaternions;
        public ushort start_hierarchies;
        public ushort start_NTTO;
        public ushort start_deformations;
        public ushort start_onlyFrames;
        public ushort start_channels;
        public ushort start_events;
        public ushort start_morphData;

        public AnimVector[] vectors;
        public AnimQuaternion[] quaternions;
        public AnimHierarchy[] hierarchies;
        public AnimNTTO[] ntto;
        public AnimOnlyFrame[] onlyFrames;
        public AnimChannel[] channels;
        public AnimNumOfNTTO[] numOfNTTO;
        public AnimFramesKFIndex[] framesKFIndex;
        public AnimKeyframe[] keyframes;
        public AnimEvent[] events;
        public AnimMorphData[] morphData;
        public AnimDeformation[] deformations;
    }
}
