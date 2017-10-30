using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScarletResource.MapObjects;

namespace ScarletChaos.Entities.Components
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
            Solid[] collisions = GameInstance.CurrentMap.Solids.ToArray();

            float finalX = entity.Location.X;
            float finalY = entity.Location.Y;

            entity.SpeedVertical += (float)Math.Sin(entity.GravityDirection) * entity.Gravity;
            entity.SpeedHorizontal += (float)Math.Cos(entity.GravityDirection) * entity.Gravity;

            entity.SpeedVertical.Clamp(-entity.SpeedVerticalMax, entity.SpeedVerticalMax);
            entity.SpeedHorizontal.Clamp(-entity.SpeedHorizontalMax, entity.SpeedHorizontalMax);
        }

        private void EntityMoveFloating(EntityPlayable entity)
        {

        }



        public const int MOVEMENT_NONE = 0;
        public const int MOVEMENT_PLATFORM = 1;
        public const int MOVEMENT_FLOATING = 2;
    }
}
