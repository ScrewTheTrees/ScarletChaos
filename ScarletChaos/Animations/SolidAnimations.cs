using ScarletResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Animations
{
    public class SolidAnimations
    {
        public static TextureContent Instance { get { return GameInstance.PrimaryGameInstance.texturePipeline; } }

        public static Animation TEST_LOAD { get { return new Animation(Instance.GetTexture(@"Icon.ico")); } }



    }
}
