using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Dtos.User
{
    public class ListUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Email { get; set; }
    }
}
