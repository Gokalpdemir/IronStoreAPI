using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Exceptions
{
    public class UserCreateFailedExceptions:Exception
    {
        public UserCreateFailedExceptions():base("Kullanıcı oluşturulurken beklenmeyen bir hatayla karşılaşıldı")
        {
            
        }

        public UserCreateFailedExceptions(string? message) : base(message)
        {
        }

        public UserCreateFailedExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
