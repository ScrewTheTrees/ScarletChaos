using ScarletChaos.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Entities
{
    /// <summary>
    /// The Player class, Both for Local and online players.
    /// </summary>
    class EntityPlayer : EntityPlayable
    {
        public new int EntityType = ENTITY_PLAYER; //Must be assigned
        public bool IsLocalPlayer = false;
        public new bool IsNpcControlled = false;

        public new Team EntityTeam = new Team(true, false, false);


        public new void Step120()
        {
            if (IsLocalPlayer) CanControl = true; else CanControl = false; //Handle Control
        }
    }
}
