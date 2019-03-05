﻿using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    /// <summary>
    /// Any visual set element of a physical object
    /// </summary>
    public interface IGeometricObject {
        IGeometricObject Clone();
        GameObject Gao {
            get;
        }
    }
}
