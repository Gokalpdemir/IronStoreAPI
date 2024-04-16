using ETıcaretAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int minute);
    }
}
