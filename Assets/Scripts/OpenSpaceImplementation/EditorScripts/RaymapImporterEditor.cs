using OpenSpaceImplementation.LevelLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace OpenSpaceImplementation.EditorScripts {

    [CustomEditor(typeof(RaymapImporter))]
    public class RaymapImporterEditor : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RaymapImporter levelLoader = (RaymapImporter)target;

            if (GUILayout.Button("Import Level")) {
                levelLoader.LoadLevel();
            }
            if (GUILayout.Button("Debug GenericResourceManagers")) {
                Debug.Log(Controller.FamilyManager.PrintResources());
            }
        }
    }
}
