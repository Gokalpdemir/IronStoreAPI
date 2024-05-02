using ETıcaretAPI.Application.Abstractions.Basket;
using ETıcaretAPI.Application.Dtos.Basket;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using ETıcaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IBasketWriteRepository _basketWriteRepository;
        private readonly IBasketReadRepository _basketReadRepository;
        private readonly IBasketItemWriteRepository _basketItemWriteRepository;
        private readonly IBasketItemReadRepository _basketItemReadRepository;

        Basket? IBasketService.GetUserActiveBasket
        {
            get
            {
                Basket? basket = ContextUser().Result;
                return basket;
            }
        }

        public BasketService(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketReadRepository basketReadRepository)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
            _basketItemReadRepository = basketItemReadRepository;
            _basketReadRepository = basketReadRepository;
        }

        public async Task<List<BasketItem>> GetBasketItemAsync()
        {
            Basket? basket = await ContextUser();
            Basket? result = await _basketReadRepository.Table
                  .Include(b => b.BasketItems)
                  .ThenInclude(bi => bi.Product)
                  .ThenInclude(p=>p.ProductImageFiles)
                  .FirstOrDefaultAsync(b => b.Id == basket.Id);
            return result.BasketItems.ToList();
        }


        public async Task addItemToBasketAsync(CreateBasketItem createBasketItem)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem basketItem = await _basketItemReadRepository.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(createBasketItem.ProductId));
                if (basketItem != null)
                {
                    basketItem.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new BasketItem()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(createBasketItem.ProductId),
                        Quantity = createBasketItem.Quantity,
                    });
                }
                await _basketItemWriteRepository.SaveAsync();

            }
        }


        public async Task RemoveBasketItemAsync(string BasketItemId)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(BasketItemId);
            if (basketItem != null)
            {
                _basketItemWriteRepository.Remove(basketItem);
                await _basketItemWriteRepository.SaveAsync();
            }

        }


        public async Task UpdateQuantityAsync(UpdateBasketItem updateBasketItem)
        {
            BasketItem? basketItem = await _basketItemReadRepository.GetByIdAsync(updateBasketItem.BasketItemId);
            if (basketItem != null)
            {
                basketItem.Quantity = updateBasketItem.Quantity;
                await _basketItemWriteRepository.SaveAsync();

            }
        }

        private async Task<Basket?> ContextUser()
        {
            var userName = _contextAccessor?.HttpContext?.User?.Identity?.Name;

            CheckIfUserExists(userName);
            Basket? _basket = null;
            AppUser? user1 = await _userManager.Users.Include(u => u.Baskets).ThenInclude(b => b.Order).FirstOrDefaultAsync(u => u.UserName == userName);

            if (user1.Baskets.Any(b => b.Order == null))
            {
                
                _basket = user1.Baskets.FirstOrDefault(b => b.Order == null);
                return _basket;
            }
            _basket = new Basket();
            user1.Baskets.Add(_basket);
            await _basketWriteRepository.SaveAsync();

            return _basket;


            //    AppUser? user = await _userManager.Users.Include(u => u.Baskets)
            //                                       //   .ThenInclude(e=>e.Order)
            //                                            .FirstOrDefaultAsync(u => u.UserName == userName);

            //    var _basket = from basket in user.Baskets
            //                  join order in _orderReadRepository.Table
            //                  on basket.Id equals order.Id into BasketOrders
            //                  from order in BasketOrders.DefaultIfEmpty()
            //                  select new
            //                  {
            //                      Basket = basket,
            //                      Order = order
            //                  };

            //    Basket? targetBasket = null;
            //    if (_basket.Any(b => b.Order is null))
            //    {
            //        targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
            //    }
            //    else
            //    {
            //        targetBasket = new();
            //        user.Baskets.Add(targetBasket);
            //    }
            //    await _basketWriteRepository.SaveAsync();
            //    return targetBasket;
        }

        private void CheckIfUserExists(String userName)
        {
            if (string.IsNullOrEmpty(userName) is true)
            {
                throw new Exception("Beklenmeyen  Hata aLındı");
            }
        }

        
    }

}

