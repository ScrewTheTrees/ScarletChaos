using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Entities
{
    /// <summary>
    /// Base class for all "playable" objects, this includes everything that the Game / Player can take control of and move around.
    /// Takes care of the KeyInputs, Health/Mana/Stats, Movement and etc...
    /// </summary>
    class EntityPlayable : Entity
    {
        public new int EntityType = ENTITY_PLAYABLE; //Must be assigned

        //Important control stuff
        public bool CanControl = true;
        public bool IsNpcControlled = true;
        public int NpcAI = 0;
        public String Name = null;

        //Movement
        public float SpeedHorizontal = 0f;
        public float SpeedVertical = 0f;
        public float SpeedHorizontalMax = 10f;
        public float SpeedVerticalMax = 10f;
        public float Friction = 0f;
        public float GravityDirection = 270f;
        public float Gravity = 0.25f;
        public float MaxGravity = 10f;

        //Damage System
        public Team EntityTeam = new Team(false,false,false);
        public float HealthMax = 10000f;
        public float FocusMax = 10000f;
        public float StaminaMax = 10000f;
        public float Health = 10000f;
        public float Focus = 10000f;
        public float Stamina = 10000f;


        //Input
        public bool[] Press = new bool[InputOptions.PRESS_MAX];
        public bool[] Pressed = new bool[InputOptions.PRESS_MAX];
        public bool[] Released = new bool[InputOptions.PRESS_MAX];

        

    }
}
