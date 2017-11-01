using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletResource.MapObjects;

namespace ScarletResource.Pipeline
{
    public class Collision
    {
        public int CollisionType = COLLISION_RECTANGLE;
        public Rectangle BoundingBox;
        public int Width, Height;
        public bool[,] CollisionPixelBoolMap;

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

        public bool CollidesWith(Collision other, int offsetX = 0, int offsetY = 0)
        {
            BoundingBox.Offset(offsetX, offsetY); //Set Bounds
            var retvar = CollidesWithExec(other);
            BoundingBox.Offset(-offsetX, -offsetY); //Reset Bounds
            return retvar;
        }
        private bool CollidesWithExec(Collision other)
        {
            if (BoundingBox.Width > 0 && BoundingBox.Height > 0)
            {
                if (BoundingBox.Intersects(other.BoundingBox)) //Rect is the largest.. but fastest calculative unit
                {
                    if (CollisionType == COLLISION_RECTANGLE && other.CollisionType == COLLISION_RECTANGLE) return true;
                    else if (CollisionType == COLLISION_PIXELMAP && other.CollisionType == COLLISION_PIXELMAP)
                    {
                        if (PixelMapCollidesWithPixelMap(other)) return true; //Both are PixelMaps so lets calculate as such :/
                    }
                    else if ((CollisionType == COLLISION_RECTANGLE && other.CollisionType == COLLISION_PIXELMAP)
                        || (CollisionType == COLLISION_PIXELMAP && other.CollisionType == COLLISION_RECTANGLE))
                    {
                        var rect = this; var pixelmap = other;
                        if (CollisionType == COLLISION_PIXELMAP) { rect = other; pixelmap = this; } //Swap
                        if (rect.BoundingBox.Contains(pixelmap.BoundingBox)) return true; //The pixelmap is inside the boundingbox entirely so no need to calculate per pixel.
                        else if (PixelMapCollidesWithRectangle(pixelmap, rect)) return true;
                    }
                }
            }
            return false;
        }
        public bool PixelMapCollidesWithPixelMap(Collision other)
        {
            int x21 = Math.Max(BoundingBox.Left, other.BoundingBox.Left) - BoundingBox.Left;
            int x22 = Math.Min(BoundingBox.Right, other.BoundingBox.Right) - BoundingBox.Left;
            int y21 = Math.Max(BoundingBox.Top, other.BoundingBox.Top) - BoundingBox.Top;
            int y22 = Math.Min(BoundingBox.Bottom, other.BoundingBox.Bottom) - BoundingBox.Top;

            int x1 = Math.Max(BoundingBox.Left, other.BoundingBox.Left) - other.BoundingBox.Left;
            int x2 = Math.Min(BoundingBox.Right, other.BoundingBox.Right) - other.BoundingBox.Left;
            int y1 = Math.Max(BoundingBox.Top, other.BoundingBox.Top) - other.BoundingBox.Top;
            int y2 = Math.Min(BoundingBox.Bottom, other.BoundingBox.Bottom) - other.BoundingBox.Top;

            //This is considered Dirty Checking by abusing 8 static points around the sprite to use as references.
            if (other.CollisionPixelBoolMap[x1, y1] == true && CollisionPixelBoolMap[x21, y21] == true) return true;
            else if (other.CollisionPixelBoolMap[x1, y2 - 1] == true && CollisionPixelBoolMap[x21, y22 - 1] == true) return true;
            else if (other.CollisionPixelBoolMap[x2 - 1, y1] == true && CollisionPixelBoolMap[x22 - 1, y22] == true) return true;
            else if (other.CollisionPixelBoolMap[x2 - 1, y2 - 1] == true && CollisionPixelBoolMap[x22 - 1, y22 - 1] == true) return true;
            else if (other.CollisionPixelBoolMap[x1 + (x2 / 2), y1] == true && CollisionPixelBoolMap[x21 + (x22 / 2), y21] == true) return true;
            else if (other.CollisionPixelBoolMap[x1 + (x2 / 2), y2 - 1] == true && CollisionPixelBoolMap[x21 + (x22 / 2), y22 - 1] == true) return true;
            else if (other.CollisionPixelBoolMap[x1, y1 + (y2 / 2)] == true && CollisionPixelBoolMap[x21, y21 + (y22 / 2)] == true) return true;
            else if (other.CollisionPixelBoolMap[x2 - 1, y1 + (y2 / 2)] == true && CollisionPixelBoolMap[x22 - 1, y21 + (y22 / 2)] == true) return true;

            //Well the dirty check didnt go through.
            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    if ((x >= 0 && x < other.CollisionPixelBoolMap.GetLength(0)) && (y >= 0 && y < other.CollisionPixelBoolMap.GetLength(1)))
                        if (other.CollisionPixelBoolMap[x, y] == true)
                            return true;
                }
            }
            return false;
        }

        /// <summary> Check if an PixelMap from this Collision collides with another Rectangle Collision </summary>
        private bool PixelMapCollidesWithRectangle(Collision PixelMap, Collision Rect, int offsetX = 0, int offsetY = 0)
        {
            int x1 = Math.Max(PixelMap.BoundingBox.Left, Rect.BoundingBox.Left);
            int x2 = Math.Min(PixelMap.BoundingBox.Right, Rect.BoundingBox.Right);

            int y1 = Math.Max(PixelMap.BoundingBox.Top, Rect.BoundingBox.Top);
            int y2 = Math.Min(PixelMap.BoundingBox.Bottom, Rect.BoundingBox.Bottom);

            // For each single pixel in the intersecting rectangle
            for (int y = y1; y < y2; y++)
            {
                for (int x = x1; x < x2; x++)
                {
                    if (Rect.BoundingBox.Contains(x, y))
                        return true;
                }
            }
            return false;
        }

        public void GeneratePixelMap(Texture2D tex)
        {
            CollisionType = COLLISION_PIXELMAP;
            CollisionPixelBoolMap = new bool[tex.Width, tex.Height];
            Width = tex.Width;
            Height = tex.Height;

            Color[] colors1D = new Color[tex.Width * tex.Height];
            tex.GetData(colors1D);

            for (int y = 0; y < tex.Height; y++)
            {
                for (int x = 0; x < tex.Width; x++)
                {
                    Color cor = colors1D[x + (y * tex.Width)];
                    if (cor.A > 20)
                    {
                        CollisionPixelBoolMap[x, y] = true;
                    }
                    else CollisionPixelBoolMap[x, y] = false;
                }
            }
        }


        public void SetLocation(int x, int y)
        {
            BoundingBox = new Rectangle(0, 0, Width, Height);
            BoundingBox.Offset(x, y);
        }
        public void AddOffset(int x, int y)
        {
            BoundingBox.Offset(x, y);
        }

        public const int COLLISION_RECTANGLE = 0;
        public const int COLLISION_PIXELMAP = 1;
    }


    /// <summary> A simple integer Offset for X/Y. </summary>
    public struct PixelOffset
    {
        public PixelOffset(int x, int y)
        {
            OffsetX = x;
            OffsetY = y;
        }
        public int OffsetX;
        public int OffsetY;
    }
}
