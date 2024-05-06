using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Order;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Services
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
            var orderCode = (new Random().NextDouble() * 100000000).ToString();
            orderCode = orderCode.Replace(",", "");
            var result = await _orderWriteRepository.AddAsync(new Domain.Entities.Order()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode,
            });
            await _orderWriteRepository.SaveAsync();

        }

        public async Task DeleteOrderAsync(string orderId)
        {
            Order order = await _orderReadRepository.GetByIdAsync(orderId);
            if(order != null)
            {
                await _orderWriteRepository.RemoveAsync(orderId);
                await _orderWriteRepository.SaveAsync();

            }
        }

        public async Task<ListOrderDto> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);

            var data = query.Skip(page * size).Take(size);
            return new ListOrderDto()
            {
                Orders = await data.Select(o=> new
                {
                    Id=o.Id,
                    CreatedDate=o.CreatedDate,
                    OrderCode=o.OrderCode,
                    TotalPrice=o.Basket.BasketItems.Sum(bi=>bi.Product.Price*bi.Quantity),
                    UserName=o.Basket.User.UserName
                }).ToListAsync(),
                TotalOrderCount=await query.CountAsync(),
            };

        }

        public async Task<OrderDto> GetOrderByIdAsync(string orderId)
        {
          Order? order=   await _orderReadRepository.Table.
                Include(o=>o.Basket)
                .ThenInclude(b=>b.User)
                .Include(o=>o.Basket)
                .ThenInclude(b=>b.BasketItems)
                .ThenInclude(bi=>bi.Product)
                .FirstOrDefaultAsync(o=>o.Id==Guid.Parse(orderId));
            return new OrderDto()
            {
                Id=order.Id.ToString(),
                Address=order.Address,
                BasketItems = order.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                OrderCode=order.OrderCode,
                UserName=order.Basket.User.UserName,
                CreatedDate=order.CreatedDate,
                Description=order.Description,
                TotalPrice=order.Basket.BasketItems.Sum(bi=>bi.Product.Price * bi.Quantity),

            };
        }
    }
}
