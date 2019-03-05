using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation.LevelLoading {

    public class KnownTypesBinder : ISerializationBinder {

        public IList<Type> KnownTypes { get; set; }

        public IList<Type> KnownTypesDictionary
        {
            get
            {
                return KnownTypes.Select(
                    (t) => {
                        return typeof(Dictionary<,>).MakeGenericType(typeof(System.String), t);
                    }
                ).ToList();
            }
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            IEnumerable<Type> KnownTypesCombined = KnownTypes.Concat(KnownTypesDictionary);
            return KnownTypesCombined.SingleOrDefault(t => t.UnderlyingSystemType.ToString() == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.UnderlyingSystemType.ToString();
        }
    }
}
