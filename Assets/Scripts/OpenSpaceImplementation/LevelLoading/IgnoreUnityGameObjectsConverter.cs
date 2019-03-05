using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSpaceImplementation.LevelLoading {

    public class IgnoreUnityGameObjectsConverter : JsonConverter {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UnityEngine.GameObject);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject.Load(reader);
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
