using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Order;
using ETıcaretAPI.Application.Dtos.Role;
using ETıcaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public  async Task<GetRolesDto> GetAllRoles(int page, int size)
        {
            var query = _roleManager.Roles;
               
            var data = query.Skip(page * size).Take(size);
            return new GetRolesDto()
            {
                Roles = data.Select(d => new
                {
                    Id = d.Id,
                    Name = d.Name,

                }).ToList(),
                TotalRoleCount = await query.CountAsync(),
            };


        }

        public async Task<GetRoleDto> GetRolesByIdAsync(string id)
        {
            AppRole? appRole = await _roleManager.FindByIdAsync(id);
            return new()
            {
                Id = appRole.Id,
                Name = appRole.Name
            };
        }
        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult ıdentityResult = await _roleManager.CreateAsync(new() { Name = name, Id = Guid.NewGuid().ToString() });
            return ıdentityResult.Succeeded;

        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            IdentityResult ıdentityResult = await _roleManager.DeleteAsync(new() { Id = id });
            return ıdentityResult.Succeeded;
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            IdentityResult ıdentityResult = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
            return ıdentityResult.Succeeded;
        }
    }
}
