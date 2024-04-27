using AutoMapper;
using ETıcaretAPI.Application.Abstractions.Basket;
using ETıcaretAPI.Application.Dtos.Basket;
using MediatR;

namespace ETıcaretAPI.Application.Features.Basket.Command.UpdateQuantity
{
    public class UpdateQuantityCommandHandler : IRequestHandler<UpdateQuantityCommandRequest, UpdateQuantityCommandResponse>
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public UpdateQuantityCommandHandler(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }

        public async Task<UpdateQuantityCommandResponse> Handle(UpdateQuantityCommandRequest request, CancellationToken cancellationToken)
        {
            await  _basketService.UpdateQuantityAsync(_mapper.Map<UpdateBasketItem>(request));
            return new();
        }
    }

}
