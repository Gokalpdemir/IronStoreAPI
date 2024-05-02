using AutoMapper;
using ETıcaretAPI.Application.Abstractions.Basket;
using ETıcaretAPI.Application.Abstractions.Hubs;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Order;
using MediatR;

namespace ETıcaretAPI.Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;
        private readonly IOrderHubService _orderHubService;

        public CreateOrderCommandHandler(IOrderService orderService, IMapper mapper, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
           await  _orderService.CreateOrderAsync( new CreateOrderDto()
           {
               Address = request.Address,
               Description = request.Description,
               BasketId=_basketService.GetUserActiveBasket?.Id.ToString()
           });
           await _orderHubService.OrderAddedMessageAsync("Yeni bir sipariş geldi!");
            return new();
        }
    }
}
