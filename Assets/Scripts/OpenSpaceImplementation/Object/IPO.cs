using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Object {
    /// <summary>
    /// IPO = Instantiated Physical Object. Used for level geometry
    /// </summary>
    public class IPO : IEngineObject {
        
        public PhysicalObject data;
        public string name = "";
        private GameObject gao;
        public GameObject Gao {
            get {
                if (gao == null) {
                    gao = new GameObject(name);
                }
                return gao;
            }
        }
    }
}
