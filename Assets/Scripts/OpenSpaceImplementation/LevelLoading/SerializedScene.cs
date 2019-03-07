using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OpenSpaceImplementation.LevelLoading {

    public class SerializedScene {

        // Scene properties

        public SerializedPersoData PersoData;
        public SerializedGraphData GraphData;
        public SerializedWorldData WorldData;
        public SerializedLightData LightData;
    }


}