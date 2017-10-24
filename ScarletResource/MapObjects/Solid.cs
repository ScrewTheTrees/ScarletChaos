using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletResource.MapObjects
{
    public class Solid
    {
        public Sprite Texture;
        public Sprite CollisionMask;

        /// <summary>Current Solid ID</summary>
        public int SolidID = 0;
        /// <summary>Is drawn to the screen.</summary>
        public bool Visible = false;
        /// <summary>Can be destroyed by projectiles.</summary>
        public bool Destructible = false;
        /// <summary>Can be jumped through (from sideways and under).</summary>
        public bool JumpThrough = false;
        /// <summary>Entities collide with this.</summary>
        public bool CollideEntity = true;
        /// <summary>Projectiles collide with this.</summary>
        public bool CollideProjectile = true;

        /// <summary>Health for Destructibles.</summary>
        public float Health = 10000f;

        public Solid(int solidID)
        {
            SolidID = solidID;
        }
        public Solid(Sprite texture, Sprite mask ,bool visible, bool destructible, bool jumpThrough, bool collideEntity, bool collideProjectile, int solidID)
        {
            Texture = texture;
            CollisionMask = mask;
            Visible = visible;
            Destructible = destructible;
            JumpThrough = jumpThrough;
            CollideEntity = collideEntity;
            CollideProjectile = collideProjectile;
            SolidID = solidID;
        }


    }
}
