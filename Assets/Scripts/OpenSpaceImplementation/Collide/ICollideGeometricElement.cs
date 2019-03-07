using UnityEngine;

namespace OpenSpaceImplementation.Collide {
    /// <summary>
    /// Subblocks of a geometric object / R3Mesh
    /// </summary>
    public interface ICollideGeometricElement {

        CollideMeshObject Mesh { get; set; }
        GameObject Gao { get; }

        ICollideGeometricElement Clone(CollideMeshObject mesh);
    }
}
