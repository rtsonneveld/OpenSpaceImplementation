using Newtonsoft.Json;
using OpenSpaceImplementation.LevelLoading;
using OpenSpaceImplementation.Visual;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenSpaceImplementation.Materials {

    public class TextureManager : GenericResourceManager<Texture2D>{
        public string TextureFolder = "Textures";

        public override Texture2D LoadResource(string texturePath)
        {
            string fullTexturePath = Path.Combine(TextureFolder, texturePath);
            //byte[] textureBytes = File.ReadAllBytes(fullTexturePath);

            string path = fullTexturePath.Replace("\\", "/");

            Texture2D newTexture = Resources.Load<Texture2D>(path);  //new Texture2D(2,2); // Dummy Dimensions
            //newTexture.LoadImage(textureBytes); // Load image data (and actual dimensions) here

            return newTexture;
        }
    }
}
