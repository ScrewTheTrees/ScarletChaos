using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletPipeline
{
    public class TextureContent
    {
        private GraphicsDevice Graphics;
        private Dictionary<string, Texture2D> LoadedTextures = new Dictionary<string, Texture2D>();
        private Texture2D DefaultTex;
        

        public TextureContent(GraphicsDevice d)
        {
            Graphics = d;
            DefaultTex = LoadContent("Icon.ico");
        }

        /// <summary> Gets the target texture... and loads it if it's not in memory. </summary>
        /// <param name="texPath">Local path from the "Assets" folder, dont include the \\ backslash</param>
        /// <returns>A texture, after it has Loaded... ofc!</returns>
        public Texture2D GetTexture(string texPath)
        {
            Texture2D tex = DefaultTex;
            
            if (LoadedTextures.ContainsKey(texPath))
            {
                tex = LoadedTextures[texPath];
            }
            else tex = LoadContent(texPath);

            return tex;
        }
        private Texture2D LoadContent(string path)
        {
            Texture2D c;
            if (File.Exists(PipeLine.ASSETS + path))
            {
                var stream = new FileStream(PipeLine.TEXTURES + path, FileMode.Open);
                c = Texture2D.FromStream(Graphics, stream); //TODO: Shit
                stream.Dispose();
            }
            else c = DefaultTex;

            return c;
        }
        public void FlushAllTextures()
        {
            foreach (var t in LoadedTextures)
            {
                t.Value.Dispose();
                LoadedTextures.Remove(t.Key);
            }

            GC.Collect(); //Good time to clear everything
        }


    }
}
