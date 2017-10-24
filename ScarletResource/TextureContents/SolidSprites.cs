using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ScarletResource.TextureContents
{
    public class SolidSprites
    {

        public SolidSprites()
        {
        }


        public static Sprite GetSprite(string TextureName)
        {
            Sprite ani;
            switch (TextureName)
            {
                case @"kirbytestwalk":
                    ani = new Sprite(TextureContent.GetTexture(@"kirbytest.png"), TextureName, 23, 23, 8, 54, 10);
                    ani.SetAnimationSpeed(10);
                    ani.Looping = true;
                    ani.Scale = new Vector2(2, 2);
                    ani.Origin = new Vector2(11, 11);
                    return ani;
                case @"Solids\SolidBlock.png":
                    var Texture = TextureContent.GetTexture(@"Solids\SolidBlock.png");
                    ani = new Sprite(Texture, TextureName);
                    ani.collision = new Collision(Texture, ani.FrameRect, Collision.COLLISION_RECTANGLE);
                    return ani;
            }


            return new Sprite(TextureContent.GetTexture(@"unknown.png"), TextureName);
        }



    }
}
