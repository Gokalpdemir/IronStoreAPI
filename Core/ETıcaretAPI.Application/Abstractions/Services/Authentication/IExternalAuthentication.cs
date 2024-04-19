
using ETıcaretAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETıcaretAPI.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<ETıcaretAPI.Application.Dtos.Token> GoogleLoginAsync(string idToken,int accessTokenLifeTime);
    }
}
