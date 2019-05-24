using OpenSpaceImplementation.Waypoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.AI {

    // Waypoints

    public partial class Perso {

        public Vector3 fn_p_stGetWpAbsolutePosition(WayPoint wp)
        {
            return wp.Position; // TODO: test
        }

        public bool Cond_IsValidWayPoint(WayPoint wp)
        {
            return wp != null; // TODO: test
        }
    }
}
