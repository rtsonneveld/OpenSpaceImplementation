using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace OpenSpaceImplementation.EditorScripts {

    [CustomEditor(typeof(LightBehaviour))]
    public class LightBehaviourEditor : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Recalculate all lights")) {
                Controller.LightManager.RecalculateSectorLighting();
            }
        }
    }
}
