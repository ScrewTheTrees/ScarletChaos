﻿using ScarletChaos.DataUtility;
using ScarletChaos.EnityAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScarletChaos.Entities.Components;

namespace ScarletChaos.Entities
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

        public EntityComponentMovement MovementType { get; set; } = new EntityComponentMovement(EntityComponentMovement.MOVEMENT_NONE);

        //Movement
        virtual public float SpeedHorizontal { get; set; } = 0f;
        virtual public float SpeedVertical { get; set; } = 0f;
        virtual public float SpeedHorizontalMax { get; set; } = 10f;
        virtual public float SpeedVerticalMax { get; set; } = 10f;
        virtual public float Friction { get; set; } = 0f;
        virtual public float GravityDirection { get; set; } = 270f;
        virtual public float Gravity { get; } = 0.25f;
        
        virtual public float GravityMod { get; set; } = 0f;

        //Entity Stats System
        virtual public int EntityTeam { get; set; } = Team.NEUTRAL;
        virtual public float HealthMax { get; set; } = 10000f;
        virtual public float FocusMax { get; set; } = 10000f;
        virtual public float StaminaMax { get; set; } = 10000f;
        virtual public float Health { get; set; } = 10000f;
        virtual public float Focus { get; set; } = 10000f;
        virtual public float Stamina { get; set; } = 10000f;

        //Input
        public bool[] Press = new bool[InputOptions.PRESS_MAX];
        public bool[] Pressed = new bool[InputOptions.PRESS_MAX];
        public bool[] Released = new bool[InputOptions.PRESS_MAX];
    }
}
