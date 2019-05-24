using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Animation.Component {

    [Serializable]
    public class AnimNTTO {
        public ushort flags;
        public ushort object_index;
        public byte unk4;
        public byte unk5;

        public AnimNTTO() {}

        public static ushort flag_isBoneNTTO = 0x00FF;
        public static ushort flag_isInvisible = 0x2;

        public bool IsInvisibleNTTO {
            get {
                if (Controller.Settings.engineVersion == Settings.EngineVersion.R3) {
                    return (flags & flag_isBoneNTTO) != 0;
                } else {
                    return (flags & flag_isInvisible) == flag_isInvisible;
                }
            }
        }

        public static int Size {
            get { return 6; }
        }

        public static bool Aligned {
            get {
                return false;
            }
        }
    }
}
