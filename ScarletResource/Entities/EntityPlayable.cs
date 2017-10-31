﻿using ScarletResource.DataUtility;
using ScarletResource.EnityAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScarletResource.Entities.Components;
using Microsoft.Xna.Framework.Graphics;
using ScarletResource;
using Microsoft.Xna.Framework;
using ScarletResource.Pipeline;

namespace ScarletResource.Entities
{
    /// <summary>
    /// Base class for all "playable" objects, this includes everything that the Game / Player can take control of and move around.
    /// Takes care of the KeyInputs, Health/Mana/Stats, Movement and etc...
    /// </summary>
    class EntityPlayable : Entity
    {
        override public int EntityType { get; set;  } = ENTITY_PLAYABLE;
        override public bool Persistent { get; set; } = true;

        //Important control stuff
        virtual public bool CanControl { get; set; } = true;
        virtual public bool IsNpcControlled { get; set; } = true;
        //public NpcAI EntityAI;
        /// <summary> Name of this Entity to be displayed </summary>
        virtual public String Name { get; set; } = null;

        virtual public EntityComponentMovement MovementType { get; set; } = new EntityComponentMovement(EntityComponentMovement.MOVEMENT_NONE);

        override public void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
                Sprite.DrawAnimation(spriteBatch, GetDrawingLocation(), Depth);

            spriteBatch.DrawString(FontContent.GetFont("FontArial16"), OnGround.ToString(), new Vector2(Location.X, Location.Y - 100), Color.White);
        }
        public override void Step120()
        {
            base.Step120();

            MovementType.EntityMove(this);
        }


        //Movement
        virtual public float SpeedHorizontal { get; set; } = 0f;
        virtual public float SpeedVertical { get; set; } = 0f;
        virtual public float SpeedHorizontalMax { get; set; } = 10f;
        virtual public float SpeedVerticalMax { get; set; } = 10f;
        virtual public float Friction { get; set; } = 0f;
        virtual public float GravityDirection { get; set; } = 270f;
        virtual public float Gravity { get; } = 0.01f;
        
        virtual public float GravityMod { get; set; } = 0f;

        virtual public float MoveSpeed { get; set; } = 2f;
        virtual public bool FaceDir { get; set; } = FACEDIR_RIGHT;
        virtual public bool CanMove { get; set; } = true;
        virtual public bool OnGround { get; set; } = false;


        //Entity Stats System
        virtual public int EntityTeam { get; set; } = Team.NEUTRAL;
        virtual public int HealthMax { get; set; } = 10000;
        virtual public int FocusMax { get; set; } = 10000;
        virtual public int StaminaMax { get; set; } = 10000;
        virtual public int Health { get; set; } = 10000;
        virtual public int Focus { get; set; } = 10000;
        virtual public int Stamina { get; set; } = 10000;

        //Input
        public bool[] Press = new bool[InputOptions.PRESS_MAX];
        public bool[] Pressed = new bool[InputOptions.PRESS_MAX];
        public bool[] Released = new bool[InputOptions.PRESS_MAX];


        public const bool FACEDIR_LEFT = false;
        public const bool FACEDIR_RIGHT = true;
    }
}
