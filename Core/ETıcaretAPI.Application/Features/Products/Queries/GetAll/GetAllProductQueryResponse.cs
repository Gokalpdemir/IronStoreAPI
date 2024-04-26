using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Commands.Queries.GetAll
{
    public class GetAllProductQueryResponse
    {
        public int TotalProductCount { get; set; }
        public object Products { get; set; }
    }
}
