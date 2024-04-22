using ETıcaretAPI.Application.Dtos.User;
using ETıcaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreatedUserDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateRefreshToken(string refreshToken,AppUser user,DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
