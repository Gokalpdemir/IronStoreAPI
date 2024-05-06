using ETıcaretAPI.Application.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrder);
        Task<ListOrderDto> GetAllOrdersAsync(int page,int size);
        Task DeleteOrderAsync(string orderId);
        Task<OrderDto> GetOrderByIdAsync(string orderId);
        
    }
}
