using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Dtos.Role
{
    public class GetRolesDto
    {
        public int TotalRoleCount { get; set; }
        public object Roles { get; set; }
    }
}
