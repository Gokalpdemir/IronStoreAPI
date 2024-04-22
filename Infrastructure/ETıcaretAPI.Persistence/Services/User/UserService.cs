using AutoMapper;
using Azure.Core;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.User;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Create;
using ETıcaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IMapper mapper, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CreatedUserDto> CreateAsync(CreateUserDto createUserDto)
        {
            AppUser user = _mapper.Map<AppUser>(createUserDto);
            user.Id = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, createUserDto.Password);
            CreatedUserDto response = new CreatedUserDto() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla kaydedilmiştir";
            else
            {
                foreach (var err in result.Errors)
                {
                    response.Message += $"{err.Code} - {err.Description}\n";
                }
            }
            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate,int addOnAccessTokenDate)
        { 
            
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else      
                throw new NotFoundUserException();
            
        }
    }
}
