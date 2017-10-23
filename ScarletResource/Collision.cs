using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource
{
    public class Collision
    {
        //int CollisionType = COLLISION_RECTANGLE;

        public Rectangle CollisionBox;
        public bool[][] CollisionMapPixel;



        public const int COLLISION_RECTANGLE = 0;
        public const int COLLISION_PIXEL = 1;
    }
}
