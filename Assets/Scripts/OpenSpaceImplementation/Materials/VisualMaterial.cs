using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation.Materials {
    public class VisualMaterial {

        public static VisualMaterial FromOffset(string offset)
        {
            return Controller.VisualMaterialManager.GetResource(offset);
        }
    }
}
