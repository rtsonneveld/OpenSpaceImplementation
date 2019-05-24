using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation.Sound {
    public class SoundEvent {
        public static SoundEvent FromID(int v)
        {
            return Controller.SoundManager.GetResource<SoundEvent>(v.ToString());
        }
    }
}
