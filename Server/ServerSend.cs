using System;
using Basic_Chess_Server.Datas;
using chess.engine.Game;

namespace Basic_Chess_Server.Server
{
    public class ServerSend
    {
        private static void SendTcpData(int toClient ,  Packet packet)
        {
            Console.WriteLine("Printing buffer");
            packet.PrintBuffer();
            Server.clients[toClient].tcp.SendData(packet);
        }

        public static void SendTcpDataToAll(Packet packet)
        {
            for(int i = 1;i <= 2; i++)
            {
                Server.clients[i].tcp.SendData(packet);
            }
        }   

        public static void SendTcpDataToAll(int exceptPlayer , Packet packet)
        {
            for(int i = 1; i < exceptPlayer;i++)
            {
                Server.clients[i].tcp.SendData(packet);
            }

            for(int i = exceptPlayer + 1; i < Server.maxPlayer;i++)
            {
                Server.clients[i].tcp.SendData(packet);
            }
        }

        public static void SendPositionChange(MovementData movementData)
        {
            using (Packet packet = new Packet(128, (int)ServerPackets.moveRequest))
            {
                packet.Write(movementData);
                SendTcpDataToAll(packet);
            }
        }

        public static void Welcome(int toClient , string message)
        {
            using (Packet welcomePacket = new Packet(256, (int)ServerPackets.welcome))
            {
                welcomePacket.Write(message);
                welcomePacket.Write(toClient);
                Console.WriteLine("Secnding welcome packet to client ID : " + toClient);
                SendTcpData(toClient, welcomePacket);
            }
        }

        public static void SendErrorMessage(int toClient , string errorMessage)
        {
            using (Packet errorPacket = new Packet(128, (int)ServerPackets.errorMessage))
            {
                errorPacket.Write(errorMessage);
                SendTcpData(toClient , errorPacket);
            }
        }

        public static void PingCameSuccesfully(int toClient)
        {
            Console.WriteLine("sending ping to client");
            using (Packet packet = new Packet(128, (int)ServerPackets.serverPingIsSuccesfull))
            {
                packet.Write(toClient);
                SendTcpData(toClient, packet);
            }
        }

        public static void MatchConnectionData(int toClient , Colours colour)
        {
            using (Packet packet = new Packet(128, (int)ServerPackets.playerMatchData))
            {
                packet.Write(colour);
                SendTcpData(toClient, packet);
            }
        }
    }
}
