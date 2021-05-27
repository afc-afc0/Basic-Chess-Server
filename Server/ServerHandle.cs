using Basic_Chess_Server.Datas;
using System;
namespace Basic_Chess_Server.Server
{
    class ServerHandle
    {

        public static void WelcomeReceived(int fromClient, Packet _packet)
        {
            int clientId = 0;
            _packet.Read(ref clientId);

            string clientMessage = "";
            _packet.Read(ref clientMessage);

            Console.WriteLine("Client Id : " + clientId + "  message :" + clientMessage);

            if (fromClient != clientId)
            {
                Console.WriteLine("Somethink bad happaned :  clientId != ourId (!!! ID MISMATCH)");
            }
        }

        public static void MatchRequest(int fromClient , Packet _packet)
        {
            int clientId = 0;
            _packet.Read(ref clientId);
            Chess.ChessController.Instance.GameRequestCame(fromClient);
        }

        public static void MoveRequest(int fromClient, Packet _packet)
        {
            int clientId = 0;
            _packet.Read(ref clientId);

            MovementData data = new MovementData();
            _packet.Read(ref data);

            if (Chess.ChessController.Instance.TryToMove(data))
            {
                Console.WriteLine("Sending MoveRequest Data");
                ServerSend.SendPositionChange(data);
                return;
            }

            Console.WriteLine("Invalid Move");
        }

        public static void PingCame(int fromClient, Packet packet)
        {
            Console.WriteLine("Ping game from client : " + fromClient);
            string ip = "";
            packet.Read(ref ip);
            ServerSend.PingCameSuccesfully(fromClient);
        }

    }
}
