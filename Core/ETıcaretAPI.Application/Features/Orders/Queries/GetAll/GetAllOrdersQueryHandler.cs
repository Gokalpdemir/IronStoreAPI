﻿using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;

namespace ETıcaretAPI.Application.Features.Orders.Queries.GetAll
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        private readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
           var data=  await _orderService.GetAllOrdersAsync(request.Page,request.Size);
            return new()
            {
                TotalOrderCount = data.TotalOrderCount,
                Orders = data.Orders
            };
        }
        
    }
}
