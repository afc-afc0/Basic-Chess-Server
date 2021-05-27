using System;
using System.Net;
using System.Net.Sockets;

namespace Basic_Chess_Server.Server
{
	public class Client
	{
		public static int dataBufferSize = 4096;
		public int id;
		string clientIp;
		public TCP tcp;

		public Client(int _id)
		{
			id = _id;
			tcp = new TCP(id);
		}

		public void SetIpAddress(string ipv4)
		{
			Console.WriteLine("Incoming connection from : " + ipv4);
			clientIp = ipv4;
		}

		public class TCP
		{
			public TcpClient socket;

			private Packet receivedPacket;
			private readonly int id;
			private NetworkStream stream;
			private byte[] receiveBuffer;

			public TCP(int _id)
			{
				id = _id;
				Console.WriteLine("id : " + id);
			}

			public void Connect(TcpClient _socket)
			{
				socket = _socket;
				socket.ReceiveBufferSize = dataBufferSize;
				socket.SendBufferSize = dataBufferSize;

				stream = socket.GetStream();

				receivedPacket = new Packet(128);
				receiveBuffer = new byte[dataBufferSize];

				stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);

				ServerSend.Welcome(id, "Welcome To Server!!!!");
			}

			public void SendData(Packet packet)
			{
				try
				{
					if (socket != null)
					{
						stream.BeginWrite(packet.GetBuffer(), 0, packet.GetBuffer().Length, null, null);
					}
				}
				catch (Exception exception)
				{
					Console.WriteLine("Error while sending the data " + exception);
				}
			}

			private void ReceiveCallback(IAsyncResult _result)
			{
				try
				{
					int _byteLength = stream.EndRead(_result);
					if (_byteLength < 0)
					{
						Console.WriteLine("Somethink bad happened");
						return;
					}

					DataHandle();
					stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
				}
				catch (Exception _ex)
				{
					Console.WriteLine($"Error receiving TCP data  : {_ex}");
				}
			}

			public void DataHandle()
			{
				ThreadManager.ExecuteOnMainThread(() =>
				{
					using (Packet incomedPacket = new Packet(128))
					{
						incomedPacket.SetBytes(receiveBuffer);

						int packetHandlerNum = 0;
						incomedPacket.Read(ref packetHandlerNum);

						Server.packetHandlers[packetHandlerNum](id, incomedPacket);
					}
				});


			}

		}


	}
}