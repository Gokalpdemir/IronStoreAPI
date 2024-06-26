﻿using ETıcaretAPI.Application.Dtos.User;
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
        Task UpdateRefreshTokenAsync(string refreshToken,AppUser user,DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUserDto>> GetAllUsersAsync(int page, int size);
        int TotalUsersCount {  get; }
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name,string code);
    }
}
