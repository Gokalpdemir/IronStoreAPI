﻿using ETıcaretAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Domain.Entities;

public class Product:BaseEntity
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
    //public ICollection<Order> Orders { get; set; }
    public ICollection<ProductImageFile> ProductImageFiles { get; set; }
    public ICollection<BasketItem>  BasketItems { get; set; }
    public Category Category { get; set; }
}
