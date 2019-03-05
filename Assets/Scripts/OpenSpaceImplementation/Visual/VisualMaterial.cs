using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenSpaceImplementation.LevelLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    /// <summary>
    /// Visual Material definition
    /// </summary>
    public class VisualMaterial {
        public List<VisualMaterialTexture> textures;
        public List<AnimatedTexture> animTextures;
        public uint flags;
        
        public Vector4 ambientCoef;
        public Vector4 diffuseCoef;
        public Vector4 specularCoef;
        public Vector4 color;
        public uint num_textures;
		
        public ushort num_animTextures;

        public byte properties;
        private Material material;
        private Material materialBillboard;

        // UV scrolling
        public int currentAnimTexture = 0;

        // flags
        public static uint flags_isTransparent = (1 << 3);
        public static uint flags_backfaceCulling = (1 << 10);
        public static uint flags_isMaterialChromed = (1 << 22);
        public static uint flags_isBillboard = (1 << 9);

        //properties
        public static uint property_receiveShadows = 2;
        public static uint property_isSpriteGenerator = 4;
        public static uint property_isAnimatedSpriteGenerator = 12;
        public static uint property_isGrass = 0x2000;
        public static uint property_isWater = 0x1000;

        public enum Hint {
            None = 0,
            Transparent = 1,
            Billboard = 2
        }

        [JsonIgnore]
        public Hint receivedHints = Hint.None;

        public bool ScrollingEnabled {
            get {
                return textures.Where(t => t != null && t.ScrollingEnabled).Count() > 0;
            }
        }

        // TODO: Split material into material_main and material_light, find how these are stored differently.
        public Material GetMaterial(Hint hints = Hint.None) {
            if (material == null || hints != receivedHints) {
                bool billboard = (hints & Hint.Billboard) == Hint.Billboard;// || (flags & flags_isBillboard) == flags_isBillboard;
                //MapLoader l = MapLoader.Loader;
                receivedHints = hints;
                //bool backfaceCulling = ((flags & flags_backfaceCulling) == flags_backfaceCulling); // example: 4DDC43FF
                Material baseMaterial = Controller.VisualMaterialManager.baseMaterial;
                bool transparent = IsTransparent || ((hints & Hint.Transparent) == Hint.Transparent) || textures.Count == 0;
                if (textures.Where(t => (t.properties & 0x20) != 0).Count() > 0 || IsLight || (textures.Count > 0 && textures[0].textureOp == 1)) {
                    baseMaterial = Controller.VisualMaterialManager.baseLightMaterial;
                } else if (transparent) {
                    baseMaterial = Controller.VisualMaterialManager.baseTransparentMaterial;
                }
                //if (textureTypes.Where(i => ((i & 1) != 0)).Count() > 0) baseMaterial = loader.baseLightMaterial;
                material = new Material(baseMaterial);
                if (textures != null) {
                    material.SetFloat("_NumTextures", num_textures);
                    if (num_textures == 0) {
                        // Zero textures? Can only happen in R3 mode. Make it fully transparent.
                        Texture2D tex = new Texture2D(1, 1);
                        tex.SetPixel(0, 0, new Color(0,0,0,0));
                        tex.Apply();
                        material.SetTexture("_Tex0", tex);
                    }
                    for (int i = 0; i < num_textures; i++) {
                        string textureName = "_Tex" + i;
                        if (textures[i].Texture != null) {
                            material.SetTexture(textureName, textures[i].Texture);
                            material.SetVector(textureName + "Params", new Vector4(textures[i].textureOp,
                                textures[i].ScrollingEnabled ? 1f : (textures[i].IsRotate ? 2f : 0f),
                                0f, textures[i].Format));
                            material.SetVector(textureName + "Params2", new Vector4(
                                textures[i].currentScrollX, textures[i].currentScrollY,
                                textures[i].ScrollX, textures[i].ScrollY));
                            //material.SetTextureOffset(textureName, new Vector2(textures[i].texture.currentScrollX, textures[i].texture.currentScrollY));
                        } else {
                            // No texture = just color. So create white texture and let that be colored by other properties.
                            Texture2D tex = new Texture2D(1, 1);
                            tex.SetPixel(0, 0, new Color(1, 1, 1, 1));
                            tex.Apply();
                            material.SetTexture(textureName, tex);
                        }
                    }
                }
                material.SetVector("_AmbientCoef", ambientCoef);
                material.SetVector("_DiffuseCoef", diffuseCoef);
                if (billboard) material.SetFloat("_Billboard", 1f);
                /* if (baseMaterial == l.baseMaterial || baseMaterial == l.baseTransparentMaterial) {
                        material.SetVector("_AmbientCoef", ambientCoef);
                        //material.SetVector("_SpecularCoef", specularCoef);
                        material.SetVector("_DiffuseCoef", diffuseCoef);
                        //material.SetVector("_Color", color);
                        //if (IsPixelShaded) material.SetFloat("_ShadingMode", 1f);
                    }*/
            }
            return material;
        }

        public bool IsTransparent {
            get {
                bool transparent = false;
                if (Controller.ControllerInstance.settings.engineVersion == Settings.EngineVersion.R3 &&
                    ((flags & flags_isTransparent) != 0 || (receivedHints & Hint.Transparent) == Hint.Transparent)) transparent = true;
                if (Controller.ControllerInstance.settings.engineVersion < Settings.EngineVersion.R3) {
                    if ((flags & 0x4000000) != 0) transparent = true;
                }
                if (transparent) {
                    if (textures.Count > 0 && textures[0] != null && textures[0].texture != null) {
                        return textures[0].texture.IsTransparent;
                    }
                    return false;
                } else return true;
            }
        }

        public bool IsLight {
            get {
                //if (R3Loader.Loader.mode == R3Loader.Mode.Rayman2PC) R3Loader.Loader.print("Flags: " + flags + "Transparent flag: " + flags_isTransparent);
                if ((flags & flags_isTransparent) != 0 || (receivedHints & Hint.Transparent) == Hint.Transparent
                    || Controller.ControllerInstance.settings.engineVersion < Settings.EngineVersion.R3) {
                    if (textures.Count > 0 && textures[0] != null && textures[0].texture != null) {
                        return textures[0].texture.IsLight;
                    }
                    return false;
                } else return true;
            }
        }

        public bool IsPixelShaded {
            get {
                if (Controller.ControllerInstance.settings.engineVersion < Settings.EngineVersion.R3) {
                    return false;
                } else {
                    if (textures.Count > 0) {
                        return textures.Where(t => t.IsPixelShaded).Count() > 0;
                    } else {
                        return false;
                    }
                }
            }
        }

        public bool IsLockedAnimatedTexture {
            get { return (properties & 1) == 1; }
        }

        public VisualMaterial() {
            textures = new List<VisualMaterialTexture>();
            animTextures = new List<AnimatedTexture>();
        }

        public static VisualMaterial FromHash(string hash)
        {
            return Controller.VisualMaterialManager.GetResource(hash);
        }


        // Call after clone
        public void Reset() {
			material = null;
			materialBillboard = null;
		}
		public VisualMaterial Clone() {
			VisualMaterial vm = (VisualMaterial)MemberwiseClone();
			vm.textures = new List<VisualMaterialTexture>(textures);
			vm.animTextures = new List<AnimatedTexture>(animTextures);
			vm.Reset();
			return vm;
		}


        public class VisualMaterialReferenceJsonConverter : JsonConverter {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(VisualMaterial);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                JObject obj = JObject.Load(reader);
                VisualMaterialReference reference = obj.ToObject<VisualMaterialReference>();
                if (reference == null || reference.Hash == null) {
                    return null;
                }
                return VisualMaterial.FromHash(reference.Hash);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
