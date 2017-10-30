using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Entities.Components
{
    /// <summary>
    /// Component in charge of Entity Movement and collisions, 
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

        }

        private void EntityMoveFloating(EntityPlayable entity)
        {

        }



        public const int MOVEMENT_NONE = 0;
        public const int MOVEMENT_PLATFORM = 1;
        public const int MOVEMENT_FLOATING = 2;
    }
}
