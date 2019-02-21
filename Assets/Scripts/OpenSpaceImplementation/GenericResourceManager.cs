using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation {
    public abstract class GenericResourceManager<T> {
        protected Dictionary<string, T> resources = new Dictionary<string, T>();

        public T AddResource(string name, T resource)
        {
            if (!resources.ContainsKey(name)) {
                resources.Add(name, resource);
                return resource;
            } else {
                Debug.LogError("Trying to add Resource " + typeof(T) + " with name " + name + ", which already exists!");

                return default;
            }
        }

        public T GetResource(string name)
        {
            if (resources.ContainsKey(name)) {
                return resources[name];
            } else {
                Debug.LogError("Trying to access Resource " + typeof(T) + " with name " + name + ", which wasn't found!");

                return default;
            }
        }
    }
}
