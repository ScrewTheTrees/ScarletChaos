using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos
{
    struct Team
    {
        public bool Player;
        public bool Enemy;
        public bool Boss;

        public Team(bool player, bool enemy, bool boss)
        {
            Player = player;
            Enemy = enemy;
            Boss = boss;
        }

    }
}
