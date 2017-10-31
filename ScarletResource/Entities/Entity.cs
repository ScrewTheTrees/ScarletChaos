using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletResource.Entities;
using ScarletResource;
using System;

namespace ScarletResource.Entities
{
    /// <summary>
    /// Parental class for all Game Entities that are used to move around and make logic happen.
    /// </summary>
    public class Entity
    {
        public Entity()
        {
            EntityID = GameInstance.GetNextEntityID();
            Create();
            GameInstance.Entities.Add(this);
        }

        virtual public ulong EntityID { get; set; }
        virtual public ulong LatestStepIndex { get; set; } = 0;
        /// <summary> The Type of entity this entity is. </summary>
        virtual public int EntityType { get; set; } = ENTITY_BASE; //Must be assigned
        virtual public Sprite Sprite { get; set; }
        /// <summary>The mask is used as an unrendered sprite that only provides collision.</summary>
        virtual public Sprite CollisionMask { get; set; }
        virtual public float Depth { get; set; } = 0f;

        /// <summary> Entites that arent visible wont perform Draw events (at all) </summary>
        virtual public bool Visible { get; set; } = true;
        /// <summary> These Entities wont be cleansed on room changes. </summary>
        virtual public bool Persistent { get; set; } = false;
        /// <summary> These Entities are Active and will execute Step commands. </summary>
        virtual public bool Active { get; set; } = true;

        public Vector2 Location = new Vector2(0, 0);
        public Vector2 PreviousLocation = new Vector2(0, 0);

        virtual public double DrawDelta { get; set; }
        virtual public double StepDelta { get; set; }

        virtual public void Create() { }
        virtual public void Destroy() { }
        virtual public void Draw(SpriteBatch spriteBatch) { }

        virtual public void StepRaw() { }
        virtual public void Step1s() { }
        virtual public void Step1() { }
        virtual public void Step10() { }
        virtual public void Step30() { }
        virtual public void Step60() { }
        virtual public void Step120() { }

        public void SetLocation(Vector2 vec) { Location.X = vec.X; Location.Y = vec.Y; PreviousLocation.X = vec.X; PreviousLocation.Y = vec.Y; }
        public void SetLocation(float x, float y) { Location.X = x; Location.Y = y; PreviousLocation.X = x; PreviousLocation.Y = y; }

        public bool CollidesWith(Entity e, int OffsetX = 0, int OffsetY = 0)
        {
            if (CollisionMask == null || e.CollisionMask == null) return false;

            return CollisionMask.CollidesWith(e.CollisionMask, OffsetX, OffsetY);
        }

        virtual public void UpdateEntityData()
        {
            PreviousLocation.X = Location.X;
            PreviousLocation.Y = Location.Y;


            if (CollisionMask != null)
                if (CollisionMask.collision != null)
                {
                    CollisionMask.FrameRect.X = (int)Location.X;
                    CollisionMask.FrameRect.Y = (int)Location.Y;
                    CollisionMask.collision.CollisionBox.X = (int)Location.X;
                    CollisionMask.collision.CollisionBox.Y = (int)Location.Y;
                }
        }

        /// <summary> This shit calculates where this thing is actually supposed to be drawn.</summary>
        public Vector2 GetDrawingPosition()
        {
            var x1 = Location.X;
            var y1 = Location.Y;
            var x2 = PreviousLocation.X;
            var y2 = PreviousLocation.Y;

            var xNew = x1 - x2;
            var yNew = y1 - y2;
            xNew = (float)(xNew * GameInstance.Delta120);
            yNew = (float)(yNew * GameInstance.Delta120);
            xNew += x1;
            yNew += y1;
            return new Vector2(xNew, yNew);
        }
        //End

        //Major / Subclass / Group / Variation
        //Format:  MSGVV
        public const int ENTITY_BASE = 0;
        public const int ENTITY_PLAYABLE = 10000;
        public const int ENTITY_PLAYER = 11000;
        public const int ENTITY_ENEMY = 12000;
        public const int ENTITY_NPC = 13000;

        public static Type GetEntityTypeFromID(int eid)
        {
            Type ret = null;

            switch (eid)
            {
                case ENTITY_PLAYER: ret = typeof(EntityPlayer); break;

                default: ret = typeof(Entity); DebugLog.LogCritical("Trying to create invalid EntityID: " + eid); break;
            }

            return ret;
        }
    }
}
