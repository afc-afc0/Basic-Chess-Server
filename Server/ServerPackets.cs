using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Chess_Server.Server
{
    public enum ServerPackets
    {
        welcome = 1,
        map = 2,
        errorMessage = 3,
        signUpComplated = 4,
        playerMatchData = 5,
        moveRequest = 6,
        serverPingIsSuccesfull = 7,
        matchDatas = 8,
    }
}
