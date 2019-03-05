using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.LevelLoading {
    public class SerializedGraphData {

        public Dictionary<string, SerializedGraphData.EGraph> Graphs;
        public Dictionary<string, SerializedGraphData.EGraphNode> GraphNodes;
        public Dictionary<string, SerializedGraphData.EWayPoint> Waypoints;

        public struct EGraphNode {
            public string wayPointReference;
            public EArc[] arcs;
        }

        public struct EArc {
            public uint capabilities;
            public int weight;
            public string targetGraphNodeReference;
        }

        public struct EWayPoint {
            public Vector3 position;
            public float radius;
        }

        public struct EGraph {
            public string[] graphNodeReferences;
        }
    }
}
