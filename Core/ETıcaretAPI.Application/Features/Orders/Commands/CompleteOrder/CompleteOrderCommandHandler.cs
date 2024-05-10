using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.CompleteOrder;
using MediatR;
using System.Linq.Expressions;

namespace ETıcaretAPI.Application.Features.Orders.Commands.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService )
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
           CompletedOrderDto completedOrderDto= await  _orderService.CompleteOrderAsync(request.Id);
            if (completedOrderDto.IsSuccess)
            {
                
                    await _mailService.SendCompletedOrderMailAsync(completedOrderDto.Email, completedOrderDto.OrderCode, completedOrderDto.OrderDate, completedOrderDto.UserName);
                
                
            }
            return new CompleteOrderCommandResponse();
        }
    }
}
