﻿using ScarletResource;
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
        public TextureContent Instance;

        public SolidSprites(TextureContent instance)
        {
            Instance = instance;
        }


        public Sprite GetSprite(string TextureName)
        {
            switch (TextureName)
            {
                case "kirbytestwalk":
                    var ani = new Sprite(Instance.GetTexture(@"kirbytest.png"), 23, 23, 8, 54, 10);
                    ani.SetAnimationSpeed(10);
                    ani.Scale = new Vector2(2, 2);
                    ani.Origin = new Vector2(11, 11);
                    return ani;
                case "":

                    break;
            }


            return new Sprite(Instance.GetTexture(@"unknown.png"));
        }



    }
}
