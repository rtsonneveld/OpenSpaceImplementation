using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation.Materials {
    public class GameMaterial {

        public static GameMaterial FromOffset(string offset)
        {
            return Controller.GameMaterialManager.GetResource(offset);
        }
    }
}
