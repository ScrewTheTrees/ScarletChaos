using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Networking
{
    public class GameSession
    {
        public bool IsHost = false;    //The host controls all Online activity from NPC and Enemies
        public bool Connected = false;
        public NetPeerConfiguration config = new NetPeerConfiguration("ScarletChaos");
        public NetPeer Network;

        public GameSession(bool isHost)
        {
            config.EnableUPnP = true;
            config.Port = 17865;
            config.MaximumConnections = 32;

            if (isHost == true)
            {
                IsHost = true;
                Connected = true;
                config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
                Network = new NetServer(config);
            }
            else
            {
                IsHost = false;
                Connected = false;
                Network = new NetClient(config);
            }
            
            Network.Start();
        }

        public void ConnectToHost()
        {
            if (IsHost == true) return;     //Hosts dont connect to other hosts stupid! (Or maybe they do...?)
            if (Connected == true) return;  //No need to connect if we are connected ._.

            NetOutgoingMessage msg = Network.CreateMessage();
            msg.Write(NetworkMessage.MSG_JOIN);
        }

        public void ReciveMessage()
        {
            NetIncomingMessage msg;
            while ((msg = Network.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        DebugLog.LogWarning("Unhandled type: " + msg.MessageType);
                        DebugLog.LogWarning("Containing: " + msg.ReadString());
                        break;
                    default:
                        
                        break;
                }
                Network.Recycle(msg);
            }
        }

        public void SendMessage(NetOutgoingMessage sendMsg)
        {
            //Network.SendMessage(sendMsg, )
        }



        public void SwitchMessages(NetIncomingMessage msg)
        {
           

        }
    }
}
