using OpenSpaceImplementation.LevelLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace OpenSpaceImplementation.EditorScripts {

    [CustomEditor(typeof(LevelLoader))]
    public class LevelLoaderEditor : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelLoader levelLoader = (LevelLoader)target;

            if (GUILayout.Button("Load Level")) {
                levelLoader.LoadLevel();
            }
        }
    }
}
