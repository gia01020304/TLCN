using Main.Model;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSocialNetworkWeb.Realtime
{
    public class CallBackPageAccessHub : Hub<FanpageConfig>
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
