using ETıcaretAPI.Application.Features.Orders.Commands.CompleteOrder;
using ETıcaretAPI.Application.Features.Orders.Commands.Create;
using ETıcaretAPI.Application.Features.Orders.Commands.Delete;
using ETıcaretAPI.Application.Features.Orders.Queries.GetAll;
using ETıcaretAPI.Application.Features.Orders.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse responses = await _mediator.Send(getAllOrdersQueryRequest);
            return Ok(responses);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] GetByIdOrderQueryRequest getByIdOrderQueryRequest)
        {
            GetByIdOrderQueryResponse response = await _mediator.Send(getByIdOrderQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] DeleteOrdeCommandRequest deleteOrdeCommandRequest)
        {
            DeleteOrdeCommandResponse response = await _mediator.Send(deleteOrdeCommandRequest);
            return Ok(response);
        }

        [HttpGet("complete-order/{Id}")]
        public async Task<IActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest completeOrderCommandRequest)
        {
            CompleteOrderCommandResponse response = await _mediator.Send(completeOrderCommandRequest);
            return Ok(response);
        }
    }
}
