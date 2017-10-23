using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ScarletResource.TextureContents
{
    public class SolidAnimations
    {
        public TextureContent Instance;

        public SolidAnimations(TextureContent instance)
        {
            Instance = instance;
        }


        public Sprite TEST_LOAD
        {
            get
            {
                var ani = new Sprite(Instance.GetTexture(@"kirbytest.png"), 23, 23, 8, 54, 10);
                ani.SetAnimationSpeed(10);
                ani.Scale = new Vector2(2,2);
                return ani;
            }
        }



    }
}
