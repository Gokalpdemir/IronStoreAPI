using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Dtos.Order
{
    public class ListOrderDto
    {
        //public string Id { get; set; }
        //public string OrderCode { get; set; }
        //public string UserName { get; set; }
        //public float TotalPrice { get; set; }
        //public DateTime CreatedDate { get; set; }
        public int TotalOrderCount { get; set; }
        public object Orders { get; set; }
    }
}
