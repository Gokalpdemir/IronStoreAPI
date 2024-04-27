using AutoMapper;
using ETıcaretAPI.Application.Dtos.Basket;
using ETıcaretAPI.Application.Features.Basket.Command.AddItemToBasket;
using ETıcaretAPI.Application.Features.Basket.Command.UpdateQuantity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Basket.Profiles
{
    public class BasketMappingProfiles : Profile
    {
        public BasketMappingProfiles()
        {
            CreateMap<AddItemToBasketCommandRequest, CreateBasketItem>().ReverseMap();
            CreateMap<UpdateQuantityCommandRequest, UpdateBasketItem>().ReverseMap();
        }
    }
}