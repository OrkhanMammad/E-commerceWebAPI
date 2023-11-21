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
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<ProductHub> _hubContext;
        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
          await  _hubContext.Clients.All.SendAsync("receiveProductAddedMessage",message);
        }
    }
}
