﻿using ETıcaretAPI.Domain.Entities.Common;
using ETıcaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Domain.Entities
{
    public class Basket :BaseEntity
    {
        public string UserId { get; set; }    
        public AppUser User { get; set; }
        public Order Order { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
