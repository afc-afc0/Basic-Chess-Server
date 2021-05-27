using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Chess_Server.Server
{
    public enum ClientSidePackets
    {
        PingServer = 6,
        WelcomeReceived = 1,
        PieceMovement = 2,
        MatchRequest = 4,
        MovementPacket = 5,
        RequestMatchesData = 7,
    }
}
