using Newtonsoft.Json;
using OpenSpaceImplementation.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Object.Properties {

    [Serializable]
    public class State {

        public int index = 0;
        public string name = null;
        public LinkedList<int> stateTransitions;
        public string cine_mapname = null;
        public string cine_name = null;
        public byte speed;
        public AnimationReference anim_ref = null;
        //public MechanicsIDCard mechanicsIDCard; TODO: Mechanics
        public State NextStateAuto;
        public byte customStateBits;
	

		public State(int index) {
            this.index = index;
        }

        public override string ToString() {
            string result = name != null ? name : ("State #"+index+" "+ Convert.ToString(customStateBits, 2).PadLeft(8, '0'));
            if (cine_name != null) result += " | " + cine_name;
            if (cine_mapname != null) result += " (" + cine_mapname + ") ";
            return result;
        }
    }
}
