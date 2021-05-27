using Basic_Chess_Server.Datas;
using Basic_Chess_Server.Server;
using board.engine;
using board.engine.Board;
using chess.engine;
using chess.engine.Entities;
using chess.engine.Game;
using System;
using System.Linq;
using System.Text;

namespace Basic_Chess_Server.Chess
{
    public class Match
    {
        ClientInformation client1;
        ClientInformation client2;
        ChessGame game;
        string board;
        int currentColour;


        public Match()
        {
            client1 = new ClientInformation();
            client2 = new ClientInformation();
            currentColour = (int)Colours.White;
            BuildGame();
        }

        public bool MoveInput(MovementData data)
        {
            Console.WriteLine("current file : " + data.currentFile + " current rank : " + data.currentRank + " destinetion file : " + data.destinetionFile + " destinetion rank : " + data.destinetionRank );

            if (currentColour != data.colour)
                return false;

            string result = "";
            try
            {
                result = game.Move(BuildValidMove(data));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            

            Console.WriteLine(result);
            Console.WriteLine(board);
            currentColour = ChangeColor();

            return true;
        }

        private int ChangeColor()
        {
            return currentColour == 0 ? 1 : 0;
        }

        private string BuildValidMove(MovementData data)
        {
            string result = "";
            //Console.WriteLine("We would have a problem in here");
            result += (char)('a' + data.currentFile);
            result += (char)('8' - data.currentRank);
            result += (char)('a' + data.destinetionFile);
            result += (char)('8' - data.destinetionRank);
            Console.WriteLine("::::" + result + "::::");

            return result;
        }

        private void BuildGame()
        {
            if (game != null)
            {
                Console.WriteLine("game is already builded");
                return;
            }
            //build game
            var checkDetectionService = AppContainer.GetService<ICheckDetectionService>();
            var engineProvider = AppContainer.GetService<IBoardEngineProvider<ChessPieceEntity>>();
            IBoardEntityFactory<ChessPieceEntity> entityFactory = AppContainer.GetService<IBoardEntityFactory<ChessPieceEntity>>();
            game = new ChessGame(engineProvider, entityFactory, checkDetectionService);
            board = new StringBoardBuilder().BuildSimpleTestBoard(game.Board);
        }

        public bool IsMatchBuilded() => game == null ? false : true;

        public void ConnectPlayer(int clientId)
        {
            if (client1.id == -1)
            {
                client1.id = clientId;
                client1.colour = Colours.White;
                Console.WriteLine("connected to white");
                ServerSend.MatchConnectionData(clientId, Colours.White);
                return;
            }

            if (client2.id == -1)
            {
                client2.id = clientId;
                client2.colour = Colours.Black;
                Console.WriteLine("connected to black");
                ServerSend.MatchConnectionData(clientId, Colours.Black);
                return;
            }

            Console.WriteLine("--MATCH BUILDING--");
            BuildGame();
        }


    }

    public class StringBoardBuilder
    {
        public string BuildSimpleTestBoard(LocatedItem<ChessPieceEntity>[,] board)
        {
            var sb = new StringBuilder();
            sb.AppendLine("  ABCDEFGH");
            for (int rank = 7; rank >= 0; rank--)
            {
                sb.Append($"{rank + 1} ");
                for (int file = 0; file < 8; file++)
                {
                    var boardPiece = board[file, rank];

                    if (boardPiece == null)
                    {
                        sb.Append(".");
                    }
                    else
                    {
                        var entity = boardPiece.Item;
                        var piece = entity.Piece == ChessPieceName.Knight ? "N" : entity.Piece.ToString().First().ToString();
                        sb.Append(entity.Player == Colours.White ? piece.ToUpper() : piece.ToLower());
                    }
                }
                sb.Append($" {rank + 1}");

                sb.AppendLine();
            }
            sb.AppendLine("  ABCDEFGH");

            return sb.ToString();
        }
    }
}
