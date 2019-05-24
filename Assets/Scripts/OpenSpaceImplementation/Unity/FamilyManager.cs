using Newtonsoft.Json;
using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Object.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Unity {

    [Serializable]
    public class FamilyManager : GenericResourceManager {

        public string FamilyFolder = "Families";

        public override object LoadResource(string familyName)
        {
            string familyPath = Path.Combine(Application.dataPath, Controller.ControllerInstance.ResourceFolder, FamilyFolder, familyName, "Family_" + familyName + ".json");
            string familyJSON = File.ReadAllText(familyPath);

            Family family = JsonConvert.DeserializeObject<Family>(familyJSON, RaymapImporter.JsonImportSettings);

            foreach(ObjectList.ObjectListReference reference in family.objectListReferences) {

                string objectListPath = Path.Combine(Application.dataPath, Controller.ControllerInstance.ResourceFolder, Controller.FamilyManager.FamilyFolder, familyName, "ObjectList_" + reference.Hash + ".json");
                string objectListJSON = File.ReadAllText(objectListPath);

                ObjectList objectList = new ObjectList();
                objectList.entries = JsonConvert.DeserializeObject<ObjectListEntry[]>(objectListJSON, RaymapImporter.JsonImportSettings);
                family.objectLists.Add(objectList);
                Controller.ObjectListManager.AddResource(reference.Hash, objectList);
            }

            return family;
        }
    }
}
