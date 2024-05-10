using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Exceptions
{
    public class OrderNotBeCompleted : Exception
    {
        public OrderNotBeCompleted():base("Sipariş Tamamlanamadı")
        {
        }

        public OrderNotBeCompleted(string? message) : base(message)
        {
        }

        public OrderNotBeCompleted(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        
    }
}
