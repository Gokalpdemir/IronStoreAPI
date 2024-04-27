using ETıcaretAPI.Application.Abstractions.Basket;
using MediatR;

namespace ETıcaretAPI.Application.Features.Basket.Command.RemoveBasketItem
{
    public class RemoveBasketItemCommandHandler : IRequestHandler<RemoveBasketItemCommandRequest, RemoveBasketItemCommandResponse>
    {
        private readonly IBasketService _basketService;

        public RemoveBasketItemCommandHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<RemoveBasketItemCommandResponse> Handle(RemoveBasketItemCommandRequest request, CancellationToken cancellationToken)
        {
          await  _basketService.RemoveBasketItemAsync(request.BasketItemId);
            return new();
        }
    }
}
