using Newtonsoft.Json;
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

    public class VisualMaterialManager : GenericResourceManager<VisualMaterial>{
        public Material baseMaterial;
        public Material baseLightMaterial;
        public Material baseTransparentMaterial;

        public string VisualMaterialFolder = "Materials";

        public override VisualMaterial LoadResource(string hash)
        {
            string materialPath = Path.Combine(Application.dataPath, Controller.ControllerInstance.ResourceFolder, VisualMaterialFolder, "VisualMaterial_"+hash+".json");
            string materialJson = File.ReadAllText(materialPath);

            return JsonConvert.DeserializeObject<VisualMaterial>(materialJson, LevelLoader.JsonImportSettingsNoConverters);
        }
    }
}
