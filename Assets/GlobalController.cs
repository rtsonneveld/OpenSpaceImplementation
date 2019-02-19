using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenSpaceImplementation.Input;

namespace OpenSpaceImplementation {

    public class GlobalController {

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
    }

}