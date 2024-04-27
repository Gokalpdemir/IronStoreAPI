using AutoMapper;
using ETıcaretAPI.Application.Abstractions.Basket;
using ETıcaretAPI.Application.Dtos.Basket;
using MediatR;

namespace ETıcaretAPI.Application.Features.Basket.Command.AddItemToBasket
{
    public class AddItemToBasketCommandHandler : IRequestHandler<AddItemToBasketCommandRequest, AddItemToBasketCommandResponse>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public AddItemToBasketCommandHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<AddItemToBasketCommandResponse> Handle(AddItemToBasketCommandRequest request, CancellationToken cancellationToken)
        {
          await  _basketService.addItemToBasketAsync(_mapper.Map<CreateBasketItem>(request));
            return new();
        }
           

    }
}
