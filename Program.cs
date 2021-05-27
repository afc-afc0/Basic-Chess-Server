using Basic_Chess_Server.Chess;
using Basic_Chess_Server.Server;
using System;
using System.Net;
using System.Threading;

namespace Basic_Chess_Server
{
    class Program
    {
        private static bool isRunning;

        static void Main(string[] args)
        {
            Ceras.CerasController cerasController = new Ceras.CerasController();
            Chess.ChessController chessController = new Chess.ChessController();

            Console.Title = "Basic Chess Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

       
            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            Console.WriteLine("public ip : " + externalip);
            //212.154.0.41 public ip adress

            Server.Server.Start(50, 26950);

            Console.WriteLine("Server Started");
            Console.ReadLine();
        }


        private static void MainThread()
        {
            Console.WriteLine("Main Thread started . Running with " + Constants.TICKS_PER_SEC + " tick per second.");

            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
