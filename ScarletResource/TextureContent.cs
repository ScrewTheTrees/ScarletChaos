﻿using Microsoft.Xna.Framework.Graphics;
using ScarletResource.TextureContents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource
{
    public class TextureContent
    {
        private static GraphicsDevice Graphics;
        private static Dictionary<string, Texture2D> LoadedTextures = new Dictionary<string, Texture2D>();
        private static Texture2D DefaultTex;


        public TextureContent(GraphicsDevice d)
        {
            Graphics = d;
            DefaultTex = LoadContent("Icon.ico");
            
        }

        /// <summary> Gets the target texture... and loads it if it's not in memory. </summary>
        /// <param name="texPath">Local path from the "Assets\Textures" folder, dont include the \\ backslash</param>
        /// <returns>A texture, after it has Loaded..</returns>
        public static Texture2D GetTexture(string texPath)
        {
            Texture2D tex = DefaultTex;

            if (LoadedTextures.ContainsKey(texPath))
            {
                tex = LoadedTextures[texPath];
                return tex;
            }
            else
            {
                return tex = LoadContent(texPath);
            }
        }
        private static Texture2D LoadContent(string path)
        {
            Texture2D c;
            if (File.Exists(PipeLine.TEXTURES + path))
            {
                var stream = new FileStream(PipeLine.TEXTURES + path, FileMode.Open);
                c = Texture2D.FromStream(Graphics, stream);
                stream.Dispose();
                LoadedTextures.Add(path, c);
            }
            else c = DefaultTex;

            return c;
        }

        public static void FlushAllTextures()
        {
            var k = LoadedTextures.ToArray();
            foreach (var t in k)
            {
                t.Value.Dispose();
                LoadedTextures.Remove(t.Key);
            }

            GC.Collect(); //Good time to clear everything
        }
    }
}
