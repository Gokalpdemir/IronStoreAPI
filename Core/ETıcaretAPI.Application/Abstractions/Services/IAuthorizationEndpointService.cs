using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        Task AssignRoleEndpoint(string[] roles, string menu, string code, Type type);
        Task<List<string>> GetRolesToEndpointAsync(string  code,string menu);
    }
}
