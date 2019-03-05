﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OpenSpaceImplementation.Visual {
    /// <summary>
    /// Texture definition
    /// </summary>
    public class TextureInfo {

        private Texture2D texture;

        public uint field0;
        public ushort field4;
        public ushort field6;

        public uint fieldC;           //
        [JsonIgnore] // pointer to opengl texture data probaly, not interesting for JSON
        public uint field10;         //
        public uint flags;          // field14
        public ushort height_;     // field18
        public ushort width_;     // field1A
        public ushort height;    // field1C
        public ushort width;    // field1E
        public uint currentScrollX;
        public uint currentScrollY;
        public uint textureScrollingEnabled;
        public uint alphaMask; // field2C
        public uint field30;
        public uint numMipmaps = 1;
        public uint field38; // from here on, do -4 for R2. r2 doesn't have mipmaps
        public uint field3C;
        public uint field40;
        public uint field44;
        public byte field48;
        public byte flagsByte;
        public string name;


        public static uint flags_isTransparent = (1 << 3);

        public bool IsTransparent {
            get {
                if (Controller.Settings.engineVersion < Settings.EngineVersion.R3) {
                    return (flags & 0x100) != 0 || (flags & (1 << 1)) != 0 || (flags & flags_isTransparent) != 0;
                } else {
                    return (flags & flags_isTransparent) != 0;
                }
            }
        }

        public bool IsLight {
            get {
                //if (Controller.Settings.engineMode == Settings.EngineMode.R2) return false;
                return (flags & (1 << 5)) != 0;
            }
        }

        public Texture2D Texture {
            get {
                if (texture==null) {
                    SetTexture(Controller.TextureManager.LoadResource(this.name));
                }
                return texture;
            }
        }

        private void SetTexture(Texture2D t)
        {
            texture = t;
            if (texture != null) {
                if (!IsRepeatU) {
                    texture.wrapModeU = TextureWrapMode.Clamp;
                }
                if (!IsRepeatV) {
                    texture.wrapModeV = TextureWrapMode.Clamp;
                }
                if (IsMirrorX && Controller.Settings.game != Settings.Game.R2Revolution) {
                    texture.wrapModeU = TextureWrapMode.Mirror;
                }
                if (IsMirrorY && Controller.Settings.game != Settings.Game.R2Revolution) {
                    if (Controller.Settings.platform == Settings.Platform.DC) {
                        Texture2D flipped = new Texture2D(texture.width, texture.height);

                        int w = texture.width;
                        int h = texture.height;


                        for (int x = 0; x < w; x++) {
                            for (int y = 0; y < h; y++) {
                                flipped.SetPixel(x, h - y - 1, texture.GetPixel(x, y));
                            }
                        }
                        flipped.Apply();
                        texture = flipped;

                        if (!IsRepeatU) {
                            texture.wrapModeU = TextureWrapMode.Clamp;
                        }
                        if (IsMirrorX) {
                            texture.wrapModeU = TextureWrapMode.Mirror;
                        }
                    }
                    texture.wrapModeV = TextureWrapMode.Mirror;
                }
                if ((flags & 0x902) != 0 && Controller.Settings.engineVersion < Settings.EngineVersion.R3) {
                    byte[] alphaMaskBytes = BitConverter.GetBytes(alphaMask);
                    SetTextureAlpha(alphaMaskBytes[0] / 255f, alphaMaskBytes[1] / 255f, alphaMaskBytes[2] / 255f);
                    /*MapLoader.Loader.print(name + " - Alpha mask: " + alphaMask + " - " + String.Format("{0:X}", alphaMask));
                    MapLoader.Loader.print("Flags & 0x10: " + ((flags & 0x10) != 0));
                    MapLoader.Loader.print("Flags & 0x808: " + ((flags & 0x808) != 0));
                    MapLoader.Loader.print("Flags & 0x902: " + ((flags & 0x902) != 0));*/
                }
            }
        }

        private void SetTextureAlpha(float r, float g, float b) {
            Color[] pixels = texture.GetPixels();
            for (int i = 0; i < pixels.Length; i++) {
                if (pixels[i].r == r && pixels[i].g == g && pixels[i].b == b) {
                    pixels[i] = new Color(r, g, b, 0);
                }
            }
            texture.SetPixels(pixels);
            texture.Apply();
        }

        // Doesn't work
        /*public void ReadTextureFromData(EndianBinaryReader reader) {
            if (off_buffer != null) {
                MapLoader.Loader.print(off_buffer);
                Pointer off_current = Pointer.Goto(ref reader, off_buffer);
                Texture2D tex2D = new Texture2D(width_, height_, TextureFormat.ARGB32, false);
                byte[] texBytes = reader.ReadBytes(width_ * height_ * 4);
                if (texBytes != null && texBytes.Length == width_ * height_ * 4) {
                    tex2D.LoadRawTextureData(texBytes);
                    tex2D.Apply();
                    Texture = tex2D;
                }
                Pointer.Goto(ref reader, off_current);
            }
        }*/

        public bool IsMirrorX {
            get { return (flagsByte & 4) != 0; }
        }

        public bool IsMirrorY {
            get { return (flagsByte & 8) != 0; }
        }
        public bool IsRepeatU {
            get {
                if (Controller.Settings.engineVersion >= Settings.EngineVersion.R3) return true;
                return (flagsByte & 2) != 0;
            }
        }
        public bool IsRepeatV {
            get {
                if (Controller.Settings.engineVersion >= Settings.EngineVersion.R3) return true;
                return (flagsByte & 1) != 0;
            }
        }

        public bool IsWaterEffect {
            get { return (name != null && name.Contains("watereffect")); }
        }

        public bool IsWaterFX {
            get { return (name != null && name.Contains("waterfx")); }
        }

        public bool IsFireFX {
            get { return (name != null && name.Contains("firefx")); }
        }

        public bool IsGrassFX {
            get { return (name != null && name.Contains("grassfx")); }
        }

        /*public static TextureInfo FromOffset(Pointer offset) {
            if (offset == null) return null;
            MapLoader l = MapLoader.Loader;
            for (int i = 0; i < l.textures.Length; i++) {
                if (l.textures[i] == null) continue;
                if (offset == l.textures[i].offset) return l.textures[i];
            }
            Debug.LogWarning("Material looked for texture with offset " + offset + ", but it wasn't found -- likely it's inline and dummy.");
            return null;
        }*/
    }
}
