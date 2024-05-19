using ETıcaretAPI.Application.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<GetRolesDto> GetAllRoles(int page, int size);
        Task<object> GetAllRolesNoPaginate();
        Task<GetRoleDto> GetRolesByIdAsync(string id);
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string id);
        Task<bool> UpdateRoleAsync(string id,string name);

    }
}
