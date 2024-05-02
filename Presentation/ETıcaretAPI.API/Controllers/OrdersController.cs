using ETıcaretAPI.Application.Features.Orders.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
          CreateOrderCommandResponse response= await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

    }
}
