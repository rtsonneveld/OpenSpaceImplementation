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

    [CustomEditor(typeof(LightManager))]
    public class LightManagerEditor : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LightManager lightManager = (LightManager)target;

            if (GUILayout.Button("Recalculate all lights")) {
                lightManager.RecalculateSectorLighting();
            }
        }
    }
}
