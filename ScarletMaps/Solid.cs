using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletPipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletMaps
{
    public class Solid
    {
        public bool Visible = false;
        public Animation texture;

        public bool Destructible = false;
        public bool CollideEntity = true;
        public bool CollideProjectile = true;

        public float Health = 10000f;
    }
}
