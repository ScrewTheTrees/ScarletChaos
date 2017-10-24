using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource.MapObjects
{
    public class Tile
    {
        public Sprite Texture;
        public Sprite CollisionMask;

        public int Depth = 0;

        public int X;
        public int Y;
        public int Width;
        public int Height;
    }
}
