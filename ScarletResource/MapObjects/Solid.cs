using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ScarletResource.Pipeline;

namespace ScarletResource.MapObjects
{
    public class Solid
    {
        public Sprite Sprite;
        //public Collision CollisionMask;
        public Vector2 Location = new Vector2(0, 0);

        public float Depth = 0.8f; //Solids are not normally drawn

        /// <summary>Current Solid ID</summary>
        public int SolidID = 0;
        /// <summary>Is drawn to the screen.</summary>
        public bool Visible = false;
        /// <summary>Can be destroyed by projectiles.</summary>
        public bool Destructible = false;
        /// <summary>Can be jumped through (NOT IMPLEMENTED YET)</summary>
        public bool JumpThrough = false;
        /// <summary>Entities collide with this.</summary>
        public bool CollideEntity = true;
        /// <summary>Projectiles collide with this.</summary>
        public bool CollideProjectile = true;

        /// <summary>Health for Destructibles are not decimal</summary>
        public int Health = 10000;


        public Solid(String mask = @"Solids\SolidBlock.png") : this(GetNewSolidID(), new Vector2(0, 0), mask) { }
        public Solid(int solidID, String mask = @"Solids\SolidBlock.png") : this(solidID, new Vector2(0, 0), mask) { }
        public Solid(Vector2 pos, String mask = @"Solids\SolidBlock.png") : this(GetNewSolidID(), pos, mask) { }

        public Solid(int solidID, Vector2 pos, String mask = @"Solids\SolidBlock.png")
        {
            SolidID = solidID;
            Location.X = pos.X;
            Location.Y = pos.Y;
            Sprite = SolidSprites.GetSprite(mask);
            //CollisionMask = new Collision(Sprite.Tex);
        }
        public Solid(Sprite Mask, bool visible, bool destructible, bool jumpThrough, bool collideEntity, bool collideProjectile, int solidID)
        {
            Sprite = Mask;
            //CollisionMask = new Collision(Sprite.Tex);
            Visible = visible;
            Destructible = destructible;
            JumpThrough = jumpThrough;
            CollideEntity = collideEntity;
            CollideProjectile = collideProjectile;
            SolidID = solidID;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.DrawAnimation(spriteBatch, Location, Depth);
        }

        public void UpdateSolidData()
        {

        }


        public static int NewSolidID = 0;
        public static int GetNewSolidID() { return NewSolidID; }

        //Version 1 writing/reading
        public void WriteToStreamV1(BinaryWriter stream)
        {
            stream.Write(Sprite.TexPath);
            stream.Write(Depth);
            stream.Write(SolidID);
            stream.Write(Visible);
            stream.Write(Destructible);
            stream.Write(JumpThrough);
            stream.Write(CollideEntity);
            stream.Write(CollideProjectile);
            stream.Write(Health);
            stream.Write((int)Location.X);
            stream.Write((int)Location.Y);
        }
        public void ReadFromStreamV1(BinaryReader stream)
        {
            Sprite = SolidSprites.GetSprite(stream.ReadString());
            Depth = stream.ReadInt32();
            SolidID = stream.ReadInt32();
            Visible = stream.ReadBoolean();
            Destructible = stream.ReadBoolean();
            JumpThrough = stream.ReadBoolean();
            CollideEntity = stream.ReadBoolean();
            CollideProjectile = stream.ReadBoolean();
            Health = stream.ReadInt32();
            Location.X = stream.ReadInt32();
            Location.Y = stream.ReadInt32();
        }
    }
}
