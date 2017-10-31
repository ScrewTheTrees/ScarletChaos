using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScarletResource.MapObjects;

namespace ScarletResource.Entities.Components
{
    /// <summary>
    /// Component in charge of EntityPlayable Movement and collisions, 
    /// </summary>
    class EntityComponentMovement
    {
        public EntityComponentMovement(int movementType)
        {
            MovementType = movementType;
        }

        public int MovementType = MOVEMENT_NONE;

        public void EntityMove(EntityPlayable entity)
        {
            if (MovementType == MOVEMENT_NONE) return;
            else if (MovementType == MOVEMENT_PLATFORM)
                EntityMovePlatformer(entity);
            else if (MovementType == MOVEMENT_FLOATING)
                EntityMoveFloating(entity);
        }

        private void EntityMovePlatformer(EntityPlayable entity)
        {
            Solid[] collisions = Map.CurrentMap.Solids
                .Where(x => x.CollideEntity == true)
                .ToArray();

            float finalX = entity.Location.X;
            float finalY = entity.Location.Y;


            float gravX = (float)((entity.Gravity + entity.GravityMod) * Math.Cos(entity.GravityDirection.DegToRad()));
            float gravY = (float)((entity.Gravity + entity.GravityMod) * -Math.Sin(entity.GravityDirection.DegToRad()));
            //Add gravity
            entity.SpeedHorizontal += gravX;
            entity.SpeedVertical += gravY;

            //Clamp
            entity.SpeedHorizontal.Clamp(-entity.SpeedHorizontalMax, entity.SpeedHorizontalMax);
            entity.SpeedVertical.Clamp(-entity.SpeedVerticalMax, entity.SpeedVerticalMax);

            //Handle Horizontal collisions
            while (CollisionSolid(entity, collisions, 0, entity.SpeedHorizontal) == true && entity.SpeedHorizontal != 0)
            {
                if (entity.SpeedHorizontal >= 1) entity.SpeedHorizontal -= 1;
                else if (entity.SpeedHorizontal <= -1) entity.SpeedHorizontal += 1;
                else entity.SpeedHorizontal = 0;
            }
            //Handle Vertical collisions
            while (CollisionSolid(entity, collisions, entity.SpeedVertical, 0) == true && entity.SpeedVertical != 0)
            {
                if (entity.SpeedVertical >= 1) entity.SpeedVertical -= 1;
                else if (entity.SpeedVertical <= -1) entity.SpeedVertical += 1;
                else entity.SpeedVertical = 0;
            }

            if (CollisionSolid(entity, collisions, gravX * 2, gravY * 2))
            {
                entity.OnGround = true;
                entity.SpeedHorizontal = 0;
                entity.SpeedVertical = 0;
            }
            else entity.OnGround = false;

            finalX += entity.SpeedHorizontal;
            finalY += entity.SpeedVertical;

            entity.Location.X = finalX;
            entity.Location.Y = finalY;
        }

        private bool CollisionSolid(Entity e, Solid[] colli, float offsetX = 0, float offsetY = 0)
        {
            foreach (Solid c in colli)
            {
                //if (e.CollisionMask != null)
                    //if (e.CollisionMask.CollidesWith(c.CollisionMask, offsetX, offsetY) == true)
                        //return true;
            }
            return false; //No collisions
        }

        private void EntityMoveFloating(EntityPlayable entity)
        {

        }



        public const int MOVEMENT_NONE = 0;
        public const int MOVEMENT_PLATFORM = 1;
        public const int MOVEMENT_FLOATING = 2;
    }
}
