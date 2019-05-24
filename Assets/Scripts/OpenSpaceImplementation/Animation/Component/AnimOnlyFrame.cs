using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimOnlyFrame {
        public ushort quaternion;
        public ushort vector;
        public ushort num_hierarchies_for_frame;
        public ushort start_hierarchies_for_frame;
        public ushort unk8;
        public ushort deformation;
        public ushort numOfNTTO;
    }
}
