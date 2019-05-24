using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimDeformation {
        public short channel;
        public ushort bone;
        public short linkChannel; // channel that is controlling/controlled by this channel
        public ushort linkBone; // controlled/controlling bone

    }
}
