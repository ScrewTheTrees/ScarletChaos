﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ScarletChaos.DataUtility;
using System;

namespace ScarletChaos
{
    /// <summary>
    /// Parental class for all Game Entities that are used to move around.
    /// </summary>
    public class Entity
    {
        public ulong EntityID;
        public ulong LatestStepIndex = 0;
        public int EntityType = ENTITY_BASE;

        public Vector2 Location = new Vector2(0, 0);
        public Vector2 PreviousLocation = new Vector2(0,0);

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

        public Vector2 GetDeltaPosition()
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




        public const int ENTITY_BASE = 0;
        public const int ENTITY_PLATFORMER = 1;

        public static Type GetEntityTypeFromID(int eid)
        {
            Type ret = null;

            switch (eid)
            {
                //Base cases
                case ENTITY_BASE: case ENTITY_PLATFORMER:
                    ret = null; break;
                
            }

            return ret;
        }

    }
}
