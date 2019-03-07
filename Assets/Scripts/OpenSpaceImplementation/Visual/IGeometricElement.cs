using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    /// <summary>
    /// Subblocks of a geometric object / R3Mesh
    /// </summary>
    public interface IGeometricElement {

        IGeometricElement Clone(MeshObject mesh);
        GameObject Gao {
            get;
        }

        MeshObject Mesh
        {
            get; set;
        }

        Vector3 AveragePosition { get; }
    }
}
