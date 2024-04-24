using EticaretAPI.SignalR.Hubs;
using ETıcaretAPI.Application.Abstractions.Hubs;
using ETıcaretAPI.Domain.Constants;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        readonly private IHubContext<ProductHub> _hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
          await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.productAddedMessage,message);
        }
    }
}
