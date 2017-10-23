using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletChaos.Entities;
using ScarletResource;
using System;

namespace ScarletChaos.Entities
{
    /// <summary>
    /// Parental class for all Game Entities that are used to move around and make logic happen.
    /// </summary>
    public class Entity
    {
        public ulong EntityID;
        public ulong LatestStepIndex = 0;
        public int EntityType = ENTITY_BASE; //Must be assigned
        public Animation Sprite;
        public Single Depth = 0;

        /// <summary> Entites that arent visible wont perform Draw events (at all) </summary>
        public bool Visible = true;
        /// <summary> These Entities wont be cleansed on room changes. </summary>
        public bool Persistent = false;
        /// <summary> These Entities are Active and will execute Step commands. </summary>
        public bool Active = false;

        public Vector2 Location = new Vector2(0, 0);
        public Vector2 PreviousLocation = new Vector2(0, 0);

        public double DrawDelta;
        public double StepDelta;

        public void Create() { }
        public void Destroy() { }
        public void Draw(SpriteBatch spriteBatch) { }

        public void StepRaw() { }
        public void Step1s() { }
        public void Step1() { }
        public void Step10() { }
        public void Step30() { }
        public void Step60() { }
        public void Step120() { }

        public void SetLocation(Vector2 vec) { Location.X = vec.X; Location.Y = vec.Y; }
        public void SetLocation(float x, float y) { Location.X = x; Location.Y = y; }

        /// <summary>Updated just before .</summary>
        public void UpdateEntityData()
        {
            PreviousLocation.X = Location.X;
            PreviousLocation.Y = Location.Y;
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
        public const int ENTITY_ENEMY = 20000;
        public const int ENTITY_NPC = 30000;

        public static Type GetEntityTypeFromID(int eid)
        {
            Type ret = null;

            switch (eid)
            {
                case ENTITY_PLAYER: ret = typeof(EntityPlayer); break;

                default: ret = null; break;
            }

            return ret;
        }
    }
}
