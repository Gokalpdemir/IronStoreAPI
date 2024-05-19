using ETıcaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Queries.GetById
{
    public class GetByIdProductQueryResponse
    {
      
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string CategoryName { get; set; }
        public List<string> ProductImageFiles { get; set; }

    }
}
