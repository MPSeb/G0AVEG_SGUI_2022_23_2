using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace G0AVEG_ADT_2022_23_1.Endpoint.Services
{
    public class SignalRHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            this.Clients.All.SendAsync("onConnected").Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            this.Clients.All.SendAsync("onDisconnected").Wait();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
