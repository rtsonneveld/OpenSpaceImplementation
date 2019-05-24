using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSpaceImplementation.AI;
using OpenSpaceImplementation.Animation;
using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Object.Properties;
using UnityEngine;

namespace OpenSpaceImplementation.Unity {

    [Serializable]
    public class PersoManager : MonoBehaviour {

        public GameObject PersoParentGao;

        public void LoadPersos(GameObject persoRoot, Dictionary<string, SerializedPersoData.EPerso> persos)
        {
            foreach(var persoPair in persos) {
                SerializedPersoData.EPerso ePerso = persoPair.Value;

                // Prepare 3d Data
                Family fam = Controller.FamilyManager.GetResource<Family>(ePerso.Family);
                Perso3dData perso3dData = new Perso3dData();
                perso3dData.Family = fam;
                //perso3dData.ObjectList = fam.objectLists[0];
                //perso3dData.StateCurrent = fam.states[0];

                // Create Gameobject and add 3d data
                GameObject persoGAO = new GameObject();
                Perso perso = persoGAO.AddComponent<Perso>();
                PersoAnimationBehaviour animationBehaviour = persoGAO.AddComponent<PersoAnimationBehaviour>();
                perso.Perso3dData = perso3dData;

                persoGAO.name = persoPair.Key;
                persoGAO.transform.parent = persoRoot.transform;

                persoGAO.transform.position = ePerso.Position;
                persoGAO.transform.rotation = Quaternion.Euler(ePerso.Rotation.x, ePerso.Rotation.y, ePerso.Rotation.z);
                persoGAO.transform.localScale = ePerso.Scale;
            }
        }
    }
}
