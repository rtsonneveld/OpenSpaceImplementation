using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimHierarchy {
        public short childChannelID;
        public short parentChannelID;
    }
}
