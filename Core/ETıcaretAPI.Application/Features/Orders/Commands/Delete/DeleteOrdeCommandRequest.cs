using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Orders.Commands.Delete
{
    public class DeleteOrdeCommandRequest:IRequest<DeleteOrdeCommandResponse>
    {
        public string Id { get; set; }
    }
}
