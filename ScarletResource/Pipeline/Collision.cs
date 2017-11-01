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
        public Rectangle BoundingBox;
        public int Width, Height;
        public List<PixelOffset> CollisionPixelMap = new List<PixelOffset>();

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
            var retvar = CollidesWithExec(other, offsetX, offsetY);
            BoundingBox.Offset(-offsetX, -offsetY); //Reset Bounds
            return retvar;
        }
        private bool CollidesWithExec(Collision other, int offsetX = 0, int offsetY = 0)
        {
            if (BoundingBox.Width > 0 && BoundingBox.Height > 0)
            {
                if (RectIntersects(other)) //Rect is the largest.. but fastest calculative unit
                {
                    if (CollisionType == COLLISION_RECTANGLE && other.CollisionType == COLLISION_RECTANGLE) return true;
                    else if (CollisionType == COLLISION_PIXELMAP && other.CollisionType == COLLISION_PIXELMAP)
                    {
                        if (BoundingBox.Contains(other.BoundingBox) || other.BoundingBox.Contains(BoundingBox)) return true; //Dont make expensive calculations if we can avoid it.
                        else if (PixelMapCollidesWithPixelMap(other, offsetX, offsetY)) return true; //Both are PixelMaps so lets calculate as such :/
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
        public bool RectIntersects(Collision other, int offsetX = 0, int offsetY = 0)
        {
            if (BoundingBox.Intersects(other.BoundingBox))
                return true;
            return false;
        }
        public bool PixelMapCollidesWithPixelMap(Collision other, int offsetX = 0, int offsetY = 0)
        {
            for (int x = 0; x < CollisionPixelMap.Count; x++)
            {
                if (PixelCollidesWithPixelMap(CollisionPixelMap[x], other, offsetX, offsetY))
                    return true; //We struck gold!
            }
            return false;
        }

        /// <summary> Check if an PixelOffset from this Collision collides with another Collision </summary>
        /// <param name="Pixel">The pixel offsetXY, not the actual pixelXY in gameworld</param>
        public bool PixelCollidesWithPixelMap(PixelOffset Pixel, Collision other, int offsetX = 0, int offsetY = 0)
        {
            if (other.CollisionPixelMap.Any(p => p.OffsetX + other.BoundingBox.X == Pixel.OffsetX + offsetX + BoundingBox.X
                                            && p.OffsetY + other.BoundingBox.Y == Pixel.OffsetY + offsetY + BoundingBox.Y))
                return true;

            return false;
        }

        /// <summary> Check if an PixelMap from this Collision collides with another Rectangle Collision </summary>
        private bool PixelMapCollidesWithRectangle(Collision PixelMap, Collision Rect, int offsetX = 0, int offsetY = 0)
        {
            if (PixelMap.CollisionPixelMap.Any(p => Rect.BoundingBox.Contains(p.OffsetX + offsetX, p.OffsetY + offsetY)))
                return true;

            return false;
        }

        public void GeneratePixelMap(Texture2D tex)
        {
            CollisionType = COLLISION_PIXELMAP;
            CollisionPixelMap.Clear();

            Color[] colors1D = new Color[tex.Width * tex.Height];
            tex.GetData(colors1D);

            for (int y = 0; y < tex.Height; y++)
            {
                for (int x = 0; x < tex.Width; x++)
                {
                    Color cor = colors1D[x + (y * tex.Width)];
                    if (cor.A > 20)
                        CollisionPixelMap.Add(new PixelOffset(x, y));
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
