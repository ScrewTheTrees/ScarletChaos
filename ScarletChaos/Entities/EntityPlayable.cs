using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Entities
{
    /// <summary>
    /// Base class for all "playable" objects, this includes everything that the Game / Player can take control of and move around.
    /// Anything between Bosses, Player, NPCs and Enemies
    /// </summary>
    class EntityPlayable : Entity
    {
        public new int EntityType = ENTITY_PLAYABLE; //Must be assigned

    }
}
