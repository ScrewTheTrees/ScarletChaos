using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Networking
{
    public class PlayerClient
    {
        public int PlayerID;
        public int Port;
        public string Name;
        public string IP;

        //Without Custom ID
        public PlayerClient(int port, string ip, string name)
        {
            PlayerID = GetNextPlayerID();
            IP = ip;
            Port = port;
            Name = name;
        }
        public PlayerClient(int playerID , int port, string ip, string name)
        {
            PlayerID = playerID;
            Port = port;
            IP = ip;
            Name = name;
        }


        private static int NextPlayerID = 0;
        private static int GetNextPlayerID()
        {
            NextPlayerID += 1;
            return NextPlayerID;
        }
    }
}
