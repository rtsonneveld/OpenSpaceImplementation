using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenSpaceImplementation.Input;
using OpenSpaceImplementation.Materials;
using OpenSpaceImplementation.Sound;
using OpenSpaceImplementation.Strings;
using System;
using OpenSpaceImplementation.Unity;

namespace OpenSpaceImplementation {

    [ExecuteAlways]
    public class Controller : MonoBehaviour {

        // Instance

        public GameObject WorldRootGAO;
        public string ResourceFolder = "";
        public Settings settings = OpenSpaceImplementation.Settings.R2PC;
        //public static bool JustStarted = false; // Is set to true on startup

        public GameObject CreateWorldRoot(string levelName)
        {
            if (WorldRootGAO != null) {
                throw new Exception("Cannot create new world root for " + levelName + " because the old one still exists!");
            }
            WorldRootGAO = new GameObject("World (" + levelName + ")");
            return WorldRootGAO;
        }

        public void DestroyWorldRoot()
        {
            if (WorldRootGAO != null) {
                GameObject.DestroyImmediate(WorldRootGAO);
            }
        }

        public void ClearAllResources()
        {
            SectorManager.Clear();
            GameMaterialManager.ClearResources();
            VisualMaterialManager.ClearResources();
            TextureManager.ClearResources();
        }

        public void Update()
        {
            /*if (JustStarted || true) {
                LightManager.RecalculateSectorLighting();
                JustStarted = false;
            }*/
        }

        // Static

        private static GameObject controllerGAO => GameObject.Find("Controller");

        public static Controller ControllerInstance => controllerGAO.GetComponent<Controller>();

        public static Settings Settings
        {
            get
            {
                return ControllerInstance.settings;
            }
        }

        // Managers

        public static Canvas TextCanvas => controllerGAO.transform.Find("TextCanvas")?.GetComponent<Canvas>();

        public static InputManager InputManager => controllerGAO.transform.Find("InputManager")?.GetComponent<InputManager>();

        public static GameMaterialManager GameMaterialManager => controllerGAO.transform.Find("GameMaterialManager")?.GetComponent<GameMaterialManager>();

        public static VisualMaterialManager VisualMaterialManager => controllerGAO.transform.Find("VisualMaterialManager")?.GetComponent<VisualMaterialManager>();

        public static SoundManager SoundManager => controllerGAO.transform.Find("SoundManager")?.GetComponent<SoundManager>();

        public static TextManager TextManager => controllerGAO.transform.Find("TextManager")?.GetComponent<TextManager>();

        public static SectorManager SectorManager => controllerGAO.transform.Find("SectorManager")?.GetComponent<SectorManager>();

        public static TextureManager TextureManager => controllerGAO.transform.Find("TextureManager")?.GetComponent<TextureManager>();

        public static LightManager LightManager => controllerGAO.transform.Find("LightManager")?.GetComponent<LightManager>();

        public static void SetDirty()
        {
            EditorFix.SetObjectDirty(ControllerInstance);
            EditorFix.SetObjectDirty(SectorManager);
            EditorFix.SetObjectDirty(LightManager);
        }
    }

}