using E_commerce.Application.Abstractions.Hubs;
using E_commerce.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.SignalR.HubServices
{
    public sealed class OrderHubService : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubContext;
        public OrderHubService(IHubContext<OrderHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task OrderAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("receiveOrderAddedMessage",message);
        }
    }
}
