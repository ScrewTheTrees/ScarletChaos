﻿using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScarletResource.Pipeline
{
    public class EntitySprites
    { 

        public static Sprite GetMask(string MaskName)
        {
            Sprite ani;
            Texture2D texture;

            switch (MaskName)
            {
                case @"Masks\MaskPlayerBase.png":
                    texture = TextureContent.GetTexture(@"Masks\MaskPlayerBase.png");
                    ani = new Sprite(texture, MaskName);
                    ani.collision = new Collision(texture, ani.FrameRect, Collision.COLLISION_RECTANGLE);
                    ani.SetOriginCenter();
                    return ani;
            }


            return new Sprite(TextureContent.GetTexture(@"unknown.png"), MaskName);
        }


        public static Sprite GetSprite(string TextureName)
        {
            Sprite ani;
            switch (TextureName)
            {
                case @"kirbytest.png":
                    ani = new Sprite(TextureContent.GetTexture(@"kirbytest.png"), TextureName, 23, 23, 8, 54, 10);
                    ani.SetAnimationSpeed(10);
                    ani.Looping = true;
                    ani.Scale = new Vector2(2, 2);
                    ani.SetOriginCenter();
                    return ani;
            }


            return new Sprite(TextureContent.GetTexture(@"unknown.png"), TextureName);
        }
    }
}
