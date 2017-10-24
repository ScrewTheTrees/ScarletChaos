using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.DataUtility
{

    //Team Type / Identifier / Minor
    //TIMM
    public struct Team
    {
        //PVE Teams
        public const int NEUTRAL = 0;
        public const int PLAYER = 1;
        public const int ENEMY = 2;
        public const int NPC = 3;

        public const int BOSS = 101;
        public const int BOSS_SIREN = 102;


        //PVP Teams
        public const int RED = 1001;
        public const int GREEN = 1002;
        public const int BLUE = 1003;
        public const int YELLOW = 1004;

        public const int TEAM_CRUSADERS = 1101;
        public const int TEAM_OVERLORD = 1102;
    }
}
