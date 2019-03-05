using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSpaceImplementation;

namespace OpenSpaceImplementation.Strings {
    public class TextReference {

        public TextReference(int stringIndex)
        {
            this.stringIndex = stringIndex;
        }

        private int stringIndex;

        public int GetStringIndex()
        {
            return this.stringIndex;
        }

        public override string ToString()
        {
            return Controller.TextManager.GetString(this.stringIndex);
        }

        public void Overwrite(string v)
        {
            Controller.TextManager.SetString(this.stringIndex, v);
        }

        public static implicit operator string(TextReference t)
        {
            return t.ToString();
        }
    }
}
