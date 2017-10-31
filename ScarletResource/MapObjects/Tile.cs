using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ScarletResource.Pipeline;

namespace ScarletResource.MapObjects
{
    public class Tile
    {
        public Sprite Texture;

        public int Depth = 0;

        public Vector2 Position = new Vector2(0, 0);
        public int Width;
        public int Height;
    }
}
