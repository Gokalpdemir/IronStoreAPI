using ETıcaretAPI.Application.Features.Basket.Command.AddItemToBasket;
using ETıcaretAPI.Application.Features.Basket.Command.RemoveBasketItem;
using ETıcaretAPI.Application.Features.Basket.Command.UpdateQuantity;
using ETıcaretAPI.Application.Features.Basket.Query.GetBasketItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class BasketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBasketItem([FromQuery] GetBasketItemQueryRequest getBasketItemQueryRequest)
        {
            List<GetBasketItemQueryResponse> response = await _mediator.Send(getBasketItemQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
          UpdateQuantityCommandResponse response=   await _mediator.Send(updateQuantityCommandRequest);
           return Ok(response);
        }

        [HttpDelete("[action]/{BasketItemId}")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
          RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}
