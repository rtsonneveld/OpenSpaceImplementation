using OpenSpaceImplementation.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.AI {

    // Text

    public partial class Perso {


        public void Proc_IntToText(TextReference textRef, int integer)
        {
            textRef.Overwrite(integer.ToString());
        }

        public void TEXT_FormatText(string text, TextReference a, TextReference b)
        {
            return; // TODO: stub
        }
    }
}
