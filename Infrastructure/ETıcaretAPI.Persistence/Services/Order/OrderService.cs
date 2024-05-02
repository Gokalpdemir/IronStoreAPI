using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Order;
using ETıcaretAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrder)
        {
          var result= await _orderWriteRepository.AddAsync(new Domain.Entities.Order()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
            });
           await _orderWriteRepository.SaveAsync();
            
        }
    }
}
