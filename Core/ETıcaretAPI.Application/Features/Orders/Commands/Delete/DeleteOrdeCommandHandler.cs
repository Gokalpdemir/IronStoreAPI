using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETıcaretAPI.Application.Features.Orders.Commands.Delete
{
    public class DeleteOrdeCommandHandler : IRequestHandler<DeleteOrdeCommandRequest, DeleteOrdeCommandResponse>
    {
        private readonly IOrderService _orderService;

        public DeleteOrdeCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<DeleteOrdeCommandResponse> Handle(DeleteOrdeCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderService.DeleteOrderAsync(request.Id);
            return new DeleteOrdeCommandResponse();
        }
    }
}
