using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenSpaceImplementation.Collide;
using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.General {
    public class GameMaterial {
       
        public uint soundMaterial;

        public VisualMaterial visualMaterial;
        public CollideMaterial collideMaterial;

        public bool Chromed { get; set; }

        public static GameMaterial FromHash(string hash)
        {
            return Controller.GameMaterialManager.GetResource(hash);
        }

        public class GameMaterialReferenceJsonConverter : JsonConverter {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(GameMaterialReference);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                JObject obj = JObject.Load(reader);
                GameMaterialReference reference = obj.ToObject<GameMaterialReference>();
                if (reference == null || reference.Hash == null) {
                    return null;
                }
                return GameMaterial.FromHash(reference.Hash);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
