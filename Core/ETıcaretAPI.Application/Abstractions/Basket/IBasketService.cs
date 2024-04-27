using ETıcaretAPI.Application.Dtos.Basket;
using ETıcaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Basket
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemAsync();
        public Task addItemToBasketAsync(CreateBasketItem createBasketItem);

        public Task UpdateQuantityAsync(UpdateBasketItem updateBasketItem);

        public Task RemoveBasketItemAsync(string BasketItemId);
    }
}
