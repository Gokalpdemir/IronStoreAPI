using AutoMapper;
using ETıcaretAPI.Application.Dtos.Order;
using ETıcaretAPI.Application.Features.Orders.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Orders.Profiles
{
    public class OrderMappingProfile:Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderCommandRequest,CreateOrderDto>().ReverseMap();
        }
    }
}
