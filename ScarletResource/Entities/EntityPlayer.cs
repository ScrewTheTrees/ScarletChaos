using ScarletResource.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ScarletResource.Entities.Components;

namespace ScarletResource.Entities
{
    /// <summary>
    /// The Player class, Both for Local and online players.
    /// </summary>
    class EntityPlayer : EntityPlayable
    {
        override public int EntityType { get; set; } = ENTITY_PLAYER; //Must be assigned
        public bool IsLocalPlayer = false;
        override public bool IsNpcControlled { get; set; } = false;

        override public EntityComponentMovement MovementType { get; set; } = new EntityComponentMovement(EntityComponentMovement.MOVEMENT_PLATFORM);

        override public int EntityTeam { get; set; } = Team.PLAYER;

        override public void Step10()
        {
            base.Step10();
            if (IsLocalPlayer) CanControl = true; else CanControl = false; //Handle Control
        }
    }
}
