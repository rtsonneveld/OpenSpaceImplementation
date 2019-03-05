using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation.Collide {
    public enum CollideType {
        None = -1,
        ZDD = 0, // Detection
        ZDM = 1, // Mechanics
        ZDE = 2, // Events (?)
        ZDR = 3  // React (physics)
    }
}
