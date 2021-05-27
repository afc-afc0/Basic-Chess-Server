using chess.engine.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Chess_Server.Chess
{
    public class ClientInformation
    {
        public int id;
        public Colours colour;

        public ClientInformation()
        {
            id = -1;
        }
    }
}
