using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ScarletResource.Pipeline
{
    public class SolidSprites
    {
        public static Sprite GetSprite(string TextureName)
        {
            Sprite ani;
            switch (TextureName)
            {
                case @"Solids\SolidBlock.png":
                    var Texture = TextureContent.GetTexture(@"Solids\SolidBlock.png");
                    ani = new Sprite(Texture, TextureName);
                    ani.collision = new Collision(Texture, ani.FrameRect, Collision.COLLISION_RECTANGLE);
                    ani.IsAnimated = false;
                    ani.SetOriginCenter();
                    return ani;
            }


            return new Sprite(TextureContent.GetTexture(@"unknown.png"), TextureName);
        }
    }
}
