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

            if (CollisionSolid(entity, Map.CurrentMap.Solids, gravX * 2, gravY * 2))
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

        private bool CollisionSolid(Entity e, List<Solid> colli, float offsetX = 0, float offsetY = 0)
        {
            if (e.Mask != null)
            {
                for (int i = 0; i < colli.Count; i++)
                {
                    var c = colli[i];

                    if (Math.Abs(c.Location.X - e.Location.X) < c.Mask.Width + 16 && Math.Abs(c.Location.Y - e.Location.Y) < c.Mask.Height + 16) 
                        if (c.Mask != null)
                            if (e.Mask.CollidesWith(c.Mask, (int)offsetX, (int)offsetY))
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
