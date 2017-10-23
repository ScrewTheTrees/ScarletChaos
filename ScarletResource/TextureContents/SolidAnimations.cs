using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource.TextureContents
{
    public class SolidAnimations
    {
        public TextureContent Instance;

        public SolidAnimations(TextureContent instance)
        {
            Instance = instance;
        }


        public Animation TEST_LOAD
        {
            get
            {
                var ani = new Animation(Instance.GetTexture(@"Icon.ico"));
                return ani;
            }
        }



    }
}
