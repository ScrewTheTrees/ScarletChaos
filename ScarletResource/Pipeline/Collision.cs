using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ScarletResource.Pipeline
{
    public class Collision
    {
        int CollisionType = COLLISION_RECTANGLE;
        public Rectangle CollisionBox;
        public bool[,] CollisionPixelMap;
        public Vector2 Location = new Vector2();

        /// <summary> Collision Instance </summary>
        /// <param name="tex">Texture</param>
        /// <param name="rect">Initial rectangle to copy from</param>
        /// <param name="col">Collision type defined in COLLISION_ Contants</param>
        public Collision(Texture2D tex, Rectangle rect, int col = COLLISION_RECTANGLE)
        {
            if (col == 0 || col == 1)
                CollisionBox = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

            if (col == 1)
            {
                CollisionPixelMap = new bool[tex.Width, tex.Height];

                Color[] colors1D = new Color[tex.Width * tex.Height];
                tex.GetData<Color>(colors1D);

                for (int y = 0; y < tex.Height; y++)
                {
                    for (int x = 0; x < tex.Width; x++)
                    {
                        Color cor = colors1D[x + (y * tex.Width)];
                        if (cor.A > 10)
                            CollisionPixelMap[x, y] = true;
                        else CollisionPixelMap[x, y] = false;
                    }
                }

            }

            CollisionType = col;
        }

        /// <summary>Collision with another Collision</summary>
        /// <param name="collisionOther">The other Collision object</param>
        /// <param name="OffsetX">Optional X Offset of collision checking.</param>
        /// <param name="OffsetY">The position offset to check the collision</param>
        /// <returns>Whenever it actually collided or not.</returns>
        public bool CollidesWith(Collision collisionOther, float OffsetX = 0, float OffsetY = 0)
        {
            Collision collisionThis = this;

            Location.X = CollisionBox.X + OffsetX;
            Location.Y = CollisionBox.Y + OffsetY;
            collisionOther.Location.X = collisionOther.CollisionBox.X;
            collisionOther.Location.Y = collisionOther.CollisionBox.Y;

            if (collisionThis.CollisionType == COLLISION_RECTANGLE && collisionOther.CollisionType == COLLISION_RECTANGLE)
            {
                if (collisionThis.CollisionBox.Intersects(collisionOther.CollisionBox)) return true;
            }
            else if (collisionThis.CollisionType == COLLISION_PIXEL && collisionOther.CollisionType == COLLISION_PIXEL)
            {
                for (int x = 0; x < CollisionPixelMap.GetLength(0); x++)
                {
                    for (int y = 0; y < CollisionPixelMap.GetLength(1); y++)
                    {
                        if (PixelCollidesWithPixelMap(new Vector2(Location.X + x, Location.Y + y), collisionOther))
                            return true; //We struck gold!
                    }
                }
            }
            else if (collisionThis.CollisionBox.Intersects(collisionOther.CollisionBox))
            { //We only need to go in here if the Collision box intersects as the collision box is always bigger or the same as the pixelmap.

                var pixel = collisionThis;
                var rect = collisionOther;

                if (collisionOther.CollisionType == COLLISION_PIXEL)
                { //Switch around if the other guy is the pixels collider.
                    pixel = collisionOther;
                    rect = collisionThis;
                }

                var comp = new Vector2(pixel.Location.X, pixel.Location.Y);

                for (int x = 0; x < CollisionPixelMap.GetLength(0); x++)
                {
                    for (int y = 0; y < CollisionPixelMap.GetLength(1); y++)
                    {
                        comp.X += x;
                        comp.Y += y;
                        if (pixel.CollisionPixelMap[x, y] == true)
                            if (rect.CollisionBox.Contains(comp))
                                return true; //We struck gold!
                    }
                }
            }
            return false;
        }

        public static bool PixelCollidesWithPixelMap(Vector2 loc, Collision col2)
        {
            for (int x = 0; x < col2.CollisionPixelMap.GetLength(0); x++)
            {
                for (int y = 0; y < col2.CollisionPixelMap.GetLength(1); y++)
                {
                    if ((int)loc.X == x && (int)loc.Y == y && col2.CollisionPixelMap[x, y] == true)
                        return true; //We struck gold!

                }
            }
            return false;
        }




        public void SetLocation(Vector2 vec, Vector2 Origin)
        {
            Location.X = vec.X + Origin.X;
            Location.Y = vec.Y + Origin.Y;
        }

        public const int COLLISION_RECTANGLE = 0;
        public const int COLLISION_PIXEL = 1;
    }
}
