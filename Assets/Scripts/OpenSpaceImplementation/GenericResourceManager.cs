using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation {
    [Serializable]
    public class GenericResourceManager : MonoBehaviour {

        [Serializable]
        public class ResourceEntry {
            public string Name;
            public object Value;

            public ResourceEntry(string name, object value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        public Type AllowedType = null;
        public List<ResourceEntry> resources = new List<ResourceEntry>();

        private bool ContainsKey(string name)
        {
            return Get<object>(name) != null;
        }

        private T Get<T>(string name)
        {
            foreach (var item in resources) {
                if (name.Equals(item.Name)) {
                    return (T)item.Value;
                }
            }

            return default;
        }

        public object AddResource(string name, object resource)
        {
            return AddResource<object>(name, resource);
        }

        public T AddResource<T>(string name, T resource)
        {
            if (name != null && !ContainsKey(name)) {
                resources.Add(new ResourceEntry(name, resource));
                return (T)resource;
            } else {
                Debug.LogError("Trying to add Resource " + typeof(T) + " with name " + name + ", which already exists!");

                return default;
            }
        }

        public T GetResource<T>(string name)
        {
            if (name!=null && ContainsKey(name)) {
                return Get<T>(name);
            } else {
                T resource = (T)LoadResource(name);
                if (resource != null) {
                    AddResource(name, resource);
                    return resource;
                } else {
                    Debug.LogError("Trying to access Resource " + typeof(T) + " with name " + name + ", which wasn't found!");
                    return default;
                }
            }
        }

        public virtual object LoadResource(string name)
        {
            return null;
        }


        public void ClearResources()
        {
            Debug.Log("Clearing all "+resources.Count+" Resources of type " + AllowedType);
            resources.Clear();
        }


        public string PrintResources()
        {
            string str = "Printing " + resources.Count + " resources of type " + AllowedType + Environment.NewLine;
            foreach (var KeyValuePair in resources) {
                str += KeyValuePair.Name + " | " + KeyValuePair.Value;
                str += Environment.NewLine;
            }

            return str;
        }
    }
}
