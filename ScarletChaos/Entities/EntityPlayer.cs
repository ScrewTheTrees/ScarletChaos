using ScarletChaos.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ScarletChaos.Entities
{
    /// <summary>
    /// The Player class, Both for Local and online players.
    /// </summary>
    class EntityPlayer : EntityPlayable
    {
        override public int EntityType { get; set; } = ENTITY_PLAYER; //Must be assigned
        public bool IsLocalPlayer = false;
        public new bool IsNpcControlled = false;

        override public int EntityTeam { get; set; } = Team.PLAYER;

        override public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.DrawAnimation(spriteBatch, GetDrawingPosition(), 0);
        }

        override public void Step10()
        {
            if (IsLocalPlayer) CanControl = true; else CanControl = false; //Handle Control
        }
    }
}
