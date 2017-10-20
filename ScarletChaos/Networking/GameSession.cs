using Lidgren.Network;
using ScarletChaos.DataUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScarletChaos.Networking
{
    public class GameSession
    {
        /// <summary> The host controls all Online activity from NPC and Enemies. </summary>
        public bool IsHost = false;
        public bool Connected = false;
        public NetPeerConfiguration config = new NetPeerConfiguration("ScarletChaos");
        public NetPeer Network;
        public List<PlayerClient> Players = new List<PlayerClient>();
        public PlayerClient Player;


        public static int[] PortInterval = { 17861, 17862, 17863, 17864, 17865, 17866, 17867, 17868, 17869 };

        public GameSession(bool isHost)
        {
            config.EnableUPnP = true;
            config.Port = PortInterval[new Random().Next(0, PortInterval.Length - 1)];
            config.EnableMessageType(NetIncomingMessageType.UnconnectedData);
            config.MaximumConnections = 32;

            Player = new PlayerClient(config.Port, "127.0.0.1", GameInstance.PrimaryGameInstance.OptionsPlayer.Name);

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

        public void ConnectToHost(string host, int port)
        {
            if (IsHost == true) return;     //Hosts dont connect to other hosts stupid! (Or maybe they do...?)
            if (Connected == true) return;  //No need to connect if we are connected ._.

            NetBuffer buffer = new NetBuffer();

            NetOutgoingMessage msg = Network.CreateMessage();
            msg.WriteVariableInt32(MSG.MSG_JOIN);
            msg.Write(Player.Name);

            Network.Connect(host, port, msg);
        }

        public void ReciveMessage()
        {
            try
            {
                NetIncomingMessage msg;
                while ((msg = Network.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.DebugMessage:
                            DebugLog.LogDebug("Unhandled type: " + msg.MessageType);
                            DebugLog.LogDebug("Containing: " + msg.ReadString());
                            break;
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                            DebugLog.LogWarning("Unhandled type: " + msg.MessageType);
                            DebugLog.LogWarning("Containing: " + msg.ReadString());
                            break;
                        case NetIncomingMessageType.ConnectionLatencyUpdated:
                            DebugLog.LogInfo("ConnectionLatency Updated: " + msg.ReadString());
                            break;
                        case NetIncomingMessageType.ConnectionApproval:
                            if (msg.ReadVariableInt32() == MSG.MSG_JOIN)
                            {
                                msg.SenderConnection.Approve();
                                var ip = msg.SenderEndPoint.Address;
                                var port = msg.SenderEndPoint.Port;
                                Players.Add(new PlayerClient(port, ip.ToString(), null));
                                DebugLog.LogInfo("Player Connected: " + msg.ReadString());
                            }
                            else msg.SenderConnection.Deny();
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            byte Status = msg.ReadByte();
                            DebugLog.LogInfo("ConnectionLatency Updated: " + msg.ReadString());
                            break;

                        case NetIncomingMessageType.UnconnectedData:
                        case NetIncomingMessageType.Data:
                            SwitchMessages(msg);
                            break;
                        default:
                            DebugLog.LogWarning("Wtf something weird occured with connection: " + msg.ReadString());
                            break;
                    }
                    Network.Recycle(msg);
                }
            }
            catch (Exception e)
            {
                DebugLog.LogCritical(e.Message);
                DebugLog.LogCritical(e.StackTrace);
            }
        }

        public void SendMessage(NetOutgoingMessage sendMsg)
        {
            //Network.SendMessage(sendMsg, )
        }



        public void SwitchMessages(NetIncomingMessage msg)
        {
            switch (msg.ReadInt32())//read the header packet
            {
                case MSG.MSG_JOIN:
                    DebugLog.LogWarning("Warning, unexpected join request.");
                    break;
            }

        }
    }
}
