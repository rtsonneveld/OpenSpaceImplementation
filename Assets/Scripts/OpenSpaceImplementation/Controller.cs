using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenSpaceImplementation.Input;
using OpenSpaceImplementation.Materials;
using OpenSpaceImplementation.Sound;
using OpenSpaceImplementation.Strings;

namespace OpenSpaceImplementation {

    public class Controller {

        private static GameObject controllerGAO
        {
            get
            {
                return GameObject.Find("GlobalController");
            }
        }

        public static Canvas TextCanvas
        {
            get
            {
                return controllerGAO.transform.Find("TextCanvas")?.GetComponent<Canvas>();
            }
        }

        public static InputManager InputManager
        {
            get
            {
                return controllerGAO.transform.Find("InputManager")?.GetComponent<InputManager>();
            }
        }

        public static GameMaterialManager GameMaterialManager
        {
            get
            {
                return controllerGAO.transform.Find("GameMaterialManager")?.GetComponent<GameMaterialManager>();
            }
        }

        public static VisualMaterialManager VisualMaterialManager
        {
            get
            {
                return controllerGAO.transform.Find("VisualMaterialManager")?.GetComponent<VisualMaterialManager>();
            }
        }

        public static SoundManager SoundManager
        {
            get
            {
                return controllerGAO.transform.Find("SoundManager")?.GetComponent<SoundManager>();
            }
        }

        public static TextManager StringsManager
        {
            get
            {
                return controllerGAO.transform.Find("StringsManager")?.GetComponent<TextManager>();
            }
        }
    }

}