﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Dtos.Basket
{
    public class CreateBasketItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
