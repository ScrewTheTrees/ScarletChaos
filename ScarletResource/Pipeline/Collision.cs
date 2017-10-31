using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScarletResource.Pipeline
{
    public class Collision
    {
        public int CollisionType = COLLISION_RECTANGLE;
        Rectangle BoundingBox;
        int Width, Height;
        public bool[,] CollisionPixelMap;

        public Collision() : this(0, 0, 0, 0) { }
        public Collision(int x, int y, int width, int height)
        {
            BoundingBox = new Rectangle(0, 0, width, height);
            BoundingBox.Offset(x, y);
            Width = width;
            Height = height;
        }
        public Collision(Texture2D tex, bool MakePixelMap = false) : this(0, 0, tex.Width, tex.Height)
        {
            if (MakePixelMap) GeneratePixelMap(tex);
        }



        public bool CollidesWith(Collision other)
        {
            if (RectIntersects(other)) //Rect is the largest calculative unit
            {
                if (CollisionType == COLLISION_RECTANGLE && other.CollisionType == COLLISION_RECTANGLE) return true;
                else if (CollisionType == COLLISION_PIXELMAP && other.CollisionType == COLLISION_PIXELMAP)
                {
                    if (BoundingBox.Contains(other.BoundingBox) || other.BoundingBox.Contains(BoundingBox)) return true; //Dont make expensive calculations if we can avoid it.
                    else if (PixelMapCollidesWithPixelMap(other)) return true; //Both are PixelMaps so lets calculate as such :/
                }
                else if ((CollisionType == COLLISION_RECTANGLE && other.CollisionType == COLLISION_PIXELMAP)
                    || (CollisionType == COLLISION_PIXELMAP && other.CollisionType == COLLISION_RECTANGLE))
                {
                    var rect = this; var pixelmap = other;
                    if (CollisionType == COLLISION_PIXELMAP) { rect = other; pixelmap = this; } //Swap
                    if (rect.BoundingBox.Contains(pixelmap.BoundingBox)) return true; //The pixelmap is inside the boundingbox entirely so no need to calculate per pixel.

                }
            }
            return false;
        }
        public bool RectIntersects(Collision other)
        {
            if (BoundingBox.Intersects(other.BoundingBox))
                return true;
            return false;
        }
        public bool PixelMapCollidesWithPixelMap(Collision other)
        {
            for (int x = 0; x < other.CollisionPixelMap.GetLength(0); x++)
            {
                for (int y = 0; y < other.CollisionPixelMap.GetLength(1); y++)
                {
                    if (CollisionPixelMap[x, y] == true)
                        if (PixelCollidesWithPixelMap(new Vector2(x + BoundingBox.X, y + BoundingBox.Y), other))
                            return true; //We struck gold!
                }
            }
            return false;
        }
        public bool PixelCollidesWithPixelMap(Vector2 Pixel, Collision other)
        {
            for (int x = 0; x < other.CollisionPixelMap.GetLength(0); x++)
            {
                for (int y = 0; y < other.CollisionPixelMap.GetLength(1); y++)
                {
                    if ((int)Pixel.X == x + other.BoundingBox.X && (int)Pixel.Y == y + other.BoundingBox.Y && other.CollisionPixelMap[x, y] == true)
                        return true; //We struck gold!
                }
            }
            return false;
        }
        public void GeneratePixelMap(Texture2D tex)
        {
            CollisionType = COLLISION_PIXELMAP;
            CollisionPixelMap = new bool[tex.Width, tex.Height];

            Color[] colors1D = new Color[tex.Width * tex.Height];
            tex.GetData<Color>(colors1D);

            for (int y = 0; y < tex.Height; y++)
            {
                for (int x = 0; x < tex.Width; x++)
                {
                    Color cor = colors1D[x + (y * tex.Width)];
                    if (cor.A > 20)
                        CollisionPixelMap[x, y] = true;
                    else CollisionPixelMap[x, y] = false;
                }
            }
        }

        public void SetOffset(int x, int y)
        {
            BoundingBox.Offset(x, y);
        }

        public const int COLLISION_RECTANGLE = 0;
        public const int COLLISION_PIXELMAP = 1;
    }
}
