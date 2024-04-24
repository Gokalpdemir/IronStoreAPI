using ETıcaretAPI.Application.Dtos;
using ETıcaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int second,AppUser user);
        string CreateRefreshToken();
    }
}
