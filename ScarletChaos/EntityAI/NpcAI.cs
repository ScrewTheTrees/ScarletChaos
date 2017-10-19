using ScarletChaos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.EnityAI
{
    struct NpcAI
    {
        EntityPlayable Target;
        int AI;

        public NpcAI(EntityPlayable target, int ai)
        {
            Target = target;
            AI = ai;
        }



        public const int AI_NONE = 0;
    }
}
