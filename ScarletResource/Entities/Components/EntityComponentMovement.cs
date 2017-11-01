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
            int w = 0, h = 0;

            if (entity.Mask != null)
            {
                h = entity.Mask.Width;
                w = entity.Mask.Width;
            }

            //By using some logic we can reduce the amount of Collideable terrain to only the immediate surroundings and things that can collide.
            //This will allow for us to greatly reduce neccessary calculations for complex movement.
            Solid[] collisions = Map.CurrentMap.Solids
                .Where(x => x.CollideEntity == true && x.Mask != null)
                        .Where(x => (Math.Abs(x.Location.X - entity.Location.X) < x.Mask.Width + w + 64)
                        && (Math.Abs(x.Location.Y - entity.Location.Y) < x.Mask.Height + h + 64)
                 ).ToArray();

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
            if (e.Mask != null)
            {
                foreach (Solid c in colli)
                {
                    if (c.Mask != null)
                        if (e.Mask.CollidesWith(c.Mask))
                            return true;
                }
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
