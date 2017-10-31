using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScarletResource.Pipeline
{
    public class SolidSprites
    {
        public static Sprite GetSprite(string TextureName)
        {
            Sprite ani;
            Texture2D texture;
            switch (TextureName)
            {
                case @"Solids\SolidBlock.png":
                    texture = TextureContent.GetTexture(@"Solids\SolidBlock.png");
                    ani = new Sprite(texture, TextureName);
                    ani.collision = new Collision(texture, ani.FrameRect, Collision.COLLISION_RECTANGLE);
                    ani.SetOriginCenter();
                    return ani;

            }


            return new Sprite(TextureContent.GetTexture(@"unknown.png"), TextureName);
        }
    }
}
