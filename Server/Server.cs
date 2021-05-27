using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;


namespace Basic_Chess_Server.Server
{
	public class Server
	{
		public static int maxPlayer { get; private set; }
		public static int portNumber { get; private set; }

		public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
		public delegate void PacketHandler(int fromClient, Packet packet);
		public static Dictionary<int, PacketHandler> packetHandlers;

		private static TcpListener tcpListener;
		private static string serverIpAddress;
		public static void Start(int _maxPlayer, int _portNum)
		{
			maxPlayer = _maxPlayer;
			portNumber = _portNum;
			using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
			{
				socket.Connect("8.8.8.8", 26950);
				IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
				serverIpAddress = endPoint.Address.ToString();
			}


			Console.WriteLine("Starting server in Ip adress : " + serverIpAddress);
			INITServerData();
			tcpListener = new TcpListener(IPAddress.Any, portNumber);
			tcpListener.Start();
			tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);
		}

		private static void TCPConnectCallBack(IAsyncResult _result)
		{
			Console.WriteLine("Connection established");
			TcpClient client = tcpListener.EndAcceptTcpClient(_result);
			tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);

			for (int i = 1; i <= maxPlayer; i++)
			{
				if (clients[i].tcp.socket == null)
				{
					clients[i].tcp.Connect(client);
					clients[i].SetIpAddress(client.Client.RemoteEndPoint.ToString());
					return;
				}
			}

			Console.WriteLine("Server is full");
		}

		private static void INITServerData()
		{
			for (int i = 1; i <= maxPlayer; i++)
			{
				clients.Add(i, new Client(i));
			}

			packetHandlers = new Dictionary<int, PacketHandler>()
			{
				{(int) ClientSidePackets.WelcomeReceived , ServerHandle.WelcomeReceived },
				{(int) ClientSidePackets.PieceMovement , ServerHandle.MoveRequest },
				{(int) ClientSidePackets.MovementPacket , ServerHandle.MoveRequest },
				{(int) ClientSidePackets.PingServer , ServerHandle.PingCame },
				{(int) ClientSidePackets.MatchRequest , ServerHandle.MatchRequest},
			};

		}

	}
}