using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ScarletResource.TextureContents;

namespace ScarletResource.MapObjects
{
    public class Solid
    {
        public Sprite CollisionMask;
        public Vector2 Position = new Vector2(0, 0);

        public int Depth = 100;

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

        /// <summary>Health for Destructibles are not decimal</summary>
        public int Health = 10000;

        public Solid(int solidID)
        {
            SolidID = solidID;
        }
        public Solid(int solidID, Vector2 pos)
        {
            SolidID = solidID;
            Position.X = pos.X;
            Position.Y = pos.Y;
        }
        public Solid(Sprite Mask, bool visible, bool destructible, bool jumpThrough, bool collideEntity, bool collideProjectile, int solidID)
        {
            CollisionMask = Mask;
            Visible = visible;
            Destructible = destructible;
            JumpThrough = jumpThrough;
            CollideEntity = collideEntity;
            CollideProjectile = collideProjectile;
            SolidID = solidID;
        }










        //Version 1 writing/reading
        public void WriteToStreamV1(BinaryWriter stream)
        {
            stream.Write(CollisionMask.TexPath);
            stream.Write(Depth);
            stream.Write(SolidID);
            stream.Write(Visible);
            stream.Write(Destructible);
            stream.Write(JumpThrough);
            stream.Write(CollideEntity);
            stream.Write(CollideProjectile);
            stream.Write(Health);
            stream.Write((int)Position.X);
            stream.Write((int)Position.Y);
        }
        public void ReadFromStreamV1(BinaryReader stream)
        {
            CollisionMask = SolidSprites.GetSprite(stream.ReadString());
            Depth = stream.ReadInt32();
            SolidID = stream.ReadInt32();
            Visible = stream.ReadBoolean();
            Destructible = stream.ReadBoolean();
            JumpThrough = stream.ReadBoolean();
            CollideEntity = stream.ReadBoolean();
            CollideProjectile = stream.ReadBoolean();
            Health = stream.ReadInt32();
            Position.X = stream.ReadInt32();
            Position.Y = stream.ReadInt32();
        }

    }
}
