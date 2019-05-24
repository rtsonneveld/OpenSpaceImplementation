﻿using UnityEngine;

namespace OpenSpaceImplementation.Object {
    /// <summary>
    /// Everything a superobject can contain.Gao gives the unity GameObject representation of it.
    /// </summary>
    public interface IEngineObject {
        GameObject Gao {
            get;
        }
    }
}
