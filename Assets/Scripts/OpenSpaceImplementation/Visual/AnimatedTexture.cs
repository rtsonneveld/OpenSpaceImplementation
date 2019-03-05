using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSpaceImplementation.Visual {
    public class AnimatedTexture {
        public float time;
		public float currentTime; // Used to update this texture

        public TextureInfo texture;

        /*public AnimatedTexture(Pointer off_texture, float time) {
            this.off_texture = off_texture;
            this.time = time;
            texture = TextureInfo.FromOffset(off_texture);
        }*/
    }
}
