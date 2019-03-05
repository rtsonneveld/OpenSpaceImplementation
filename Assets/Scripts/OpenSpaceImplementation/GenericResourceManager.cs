using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation {
    public abstract class GenericResourceManager<T> : MonoBehaviour {
        protected Dictionary<string, T> resources = new Dictionary<string, T>();

        public T AddResource(string name, T resource)
        {
            if (name != null && !resources.ContainsKey(name)) {
                resources.Add(name, resource);
                return resource;
            } else {
                Debug.LogError("Trying to add Resource " + typeof(T) + " with name " + name + ", which already exists!");

                return default;
            }
        }

        public T GetResource(string name)
        {
            if (name!=null && resources.ContainsKey(name)) {
                return resources[name];
            } else {
                T resource = LoadResource(name);
                if (resource != null) {
                    AddResource(name, resource);
                    return resource;
                } else {
                    Debug.LogError("Trying to access Resource " + typeof(T) + " with name " + name + ", which wasn't found!");
                    return default;
                }
            }
        }

        public virtual T LoadResource(string name)
        {
            return default; // null
        }


        public void ClearResources()
        {
            Debug.Log("Clearing all "+resources.Count+" Resources of type " + typeof(T));
            resources.Clear();
        }
    }
}
