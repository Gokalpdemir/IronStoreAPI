using ETıcaretAPI.Application.Const;
using ETıcaretAPI.Application.CustomAttributes;
using ETıcaretAPI.Application.Enums;
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
        [AuthorizeDefinition(Menu =AuthorizeDefinitionConstants.Baskets,ActionType =ActionType.Reading,Definition ="Get Basket Items")]
        public async Task<IActionResult> GetBasketItem([FromQuery] GetBasketItemQueryRequest getBasketItemQueryRequest)
        {
            List<GetBasketItemQueryResponse> response = await _mediator.Send(getBasketItemQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Writing, Definition = "Add Item To Basket")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest addItemToBasketCommandRequest)
        {
            AddItemToBasketCommandResponse response = await _mediator.Send(addItemToBasketCommandRequest);
            return Ok(response);
        }


        [HttpPut("[action]")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Updating, Definition = "Update Quantity")]

        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityCommandRequest updateQuantityCommandRequest)
        {
          UpdateQuantityCommandResponse response=   await _mediator.Send(updateQuantityCommandRequest);
           return Ok(response);
        }

        [HttpDelete("[action]/{BasketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Item")]

        public async Task<IActionResult> RemoveBasketItem([FromRoute]RemoveBasketItemCommandRequest removeBasketItemCommandRequest)
        {
          RemoveBasketItemCommandResponse response = await _mediator.Send(removeBasketItemCommandRequest);
            return Ok(response);
        }
    }
}
