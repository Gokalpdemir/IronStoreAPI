using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.AppRoles.Commands.Create
{
    public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}
