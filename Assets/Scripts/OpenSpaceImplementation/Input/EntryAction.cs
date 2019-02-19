using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Input {
    public abstract class EntryAction {

        private bool validated = false;
        private int framesValid = -1;
        private int framesInvalid = -1;

        public void Update()
        {
            validated = Check();

            if (validated) {
                framesValid++;
                framesInvalid = -1;
            } else {
                framesInvalid++;
                framesValid = -1;
            }
        }

        public bool IsValidated()
        {
            return validated;
        }

        public bool IsInvalidated()
        {
            return !validated;
        }

        public bool IsJustValidated()
        {
            return validated && framesValid == 0;
        }

        public bool IsJustInvalidated()
        {
            return (!validated) && framesInvalid == 0;
        }

        public abstract bool Check();
    }
}
