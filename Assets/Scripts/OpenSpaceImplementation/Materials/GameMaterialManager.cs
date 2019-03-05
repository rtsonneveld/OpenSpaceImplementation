using Newtonsoft.Json;
using OpenSpaceImplementation.General;
using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Materials {

    public class GameMaterialManager : GenericResourceManager<GameMaterial> {

        public string GameMaterialFolder = "Common\\Materials";

        public override GameMaterial LoadResource(string hash)
        {
            string materialPath = Path.Combine(Application.dataPath, Controller.ControllerInstance.ResourceFolder, GameMaterialFolder, "GameMaterial_" + hash + ".json");
            string materialJson = File.ReadAllText(materialPath);

            var settings = LevelLoader.JsonImportSettings;
            settings.Converters.Add(new VisualMaterial.VisualMaterialReferenceJsonConverter());

            return JsonConvert.DeserializeObject<GameMaterial>(materialJson, settings);
        }
    }
}
