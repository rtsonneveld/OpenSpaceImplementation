using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimMorphData {

        public ushort objectIndexTo;
        public byte morphProgress;
		public short channel;
        public ushort frame;
        public byte byte6;
        public byte byte7;
        public float morphProgressFloat
        {
            get
            {
                return ((float)morphProgress) / 100.0f;
            }
        }

        public AnimMorphData() {}
    }
}
