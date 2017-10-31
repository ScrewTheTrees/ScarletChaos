﻿using Microsoft.Xna.Framework.Graphics;
using ScarletResource.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace ScarletResource.Pipeline
{
    public class FontContent : PipeLine
    {
        private static ContentManager CM = GameInstance.PrimaryGameInstance.Content;
        private static Dictionary<string, SpriteFont> LoadedFonts = new Dictionary<string, SpriteFont>();
        private static SpriteFont DefaultFont;


        /// <summary> Gets the target font... and loads it if it's not in memory. </summary>
        /// <param name="fontPath">Local path from the "Assets\Fonts" folder, dont include the \\ backslash</param>
        /// <returns>A texture, after it has Loaded..</returns>
        public static SpriteFont GetFont(string fontPath)
        {
            if (DefaultFont == null) DefaultFont = GetFont(@"FontArial16");
            SpriteFont tex = DefaultFont;

            if (LoadedFonts.ContainsKey(fontPath))
            {
                tex = LoadedFonts[fontPath];
                return tex;
            }
            else
            {
                return tex = LoadContent(fontPath);
            }
        }
        private static SpriteFont LoadContent(string path)
        {
            SpriteFont c;
            if (File.Exists(FONTS + path + ".xnb"))
            {
                c = CM.Load<SpriteFont>(@"Fonts\" + path);
                LoadedFonts.Add(path, c);
            }
            else c = DefaultFont;

            return c;
        }

        public static void FlushAllFonts()
        {
            var k = LoadedFonts.ToArray();
            foreach (var t in k)
            {
                t.Value.Texture.Dispose();
                LoadedFonts.Remove(t.Key);
            }

            GC.Collect(); //Good time to clear everything
        }
    }
}
