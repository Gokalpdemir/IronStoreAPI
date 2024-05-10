using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETıcaretAPI.Application.Features.Orders.Queries.GetById
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetByIdOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
          var data = await _orderService.GetOrderByIdAsync(request.Id);
            return new GetByIdOrderQueryResponse
            {
                Address = data.Address,
                Id = data.Id,
                BasketItems = data.BasketItems,
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                OrderCode = data.OrderCode,
                TotalPrice = data.TotalPrice,
                UserName = data.UserName,

                IsCompleted = data.IsCompleted,
            };
        }
    }
}
