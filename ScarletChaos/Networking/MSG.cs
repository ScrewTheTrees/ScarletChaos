using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Networking
{
    class MSG
    {
        public const int MSG_DEADPACKET = 0;

        public const int MSG_JOIN = 100;
        public const int MSG_JOIN_HANDSHAKE = 101;
        public const int MSG_LEAVE = 110;
        public const int MSG_LEAVE_HANDSHAKE = 111;
        public const int MSG_REQUEST_ENTITYID = 120;
        public const int MSG_SEND_PLAYERINFO = 130;

    }
}
