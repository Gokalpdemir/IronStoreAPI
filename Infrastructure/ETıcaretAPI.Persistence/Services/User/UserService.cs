using AutoMapper;
using Azure.Core;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.User;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Create;
using ETıcaretAPI.Application.Helpers;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using ETıcaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEndpointReadRepository _endpointReadRepository;

        public int TotalUsersCount => _userManager.Users.Count();

        public UserService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IEndpointReadRepository endpointReadRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _endpointReadRepository = endpointReadRepository;
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


        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
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

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                {
                    throw new PasswordChangeFaildException();
                }
            }
        }

        public async Task<List<ListUserDto>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users.Select(u => new ListUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                TwoFactorEnabled = u.TwoFactorEnabled,
                NameSurname = u.NameSurname
            }).ToList();
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);

            }

        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser? user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);

            }
            if (user != null)
            {

                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToArray();

            }
            throw new NotFoundUserException("Kullanıcı Bulunamadı");

        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            Endpoint? endPoint = await _endpointReadRepository.Table.Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == code);
            if (endPoint == null)
                return true;

            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;
            

            var endpointRoles = endPoint.Roles.Select(e => e.Name).ToArray();
            
            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                {
                    if (userRole == endpointRole)
                        return true;
                }
            }
            return false;
        }
    }
}
