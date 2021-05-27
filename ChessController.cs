using Basic_Chess_Server.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Chess_Server.Chess
{
    public class ChessController
    {
        private static ChessController instance;

        public static ChessController Instance
        {
            get
            {
                return instance;
            }
        }

        private Match match;

        public ChessController()
        {
            instance = this;
            match = new Match();
        }

        public void GameRequestCame(int fromClient)
        {
            //if(match.IsMatchBuilded() == false)
                match.ConnectPlayer(fromClient);
        }

        public bool TryToMove(MovementData data)
        {
            return match.MoveInput(data);
        }

        

    }
}
