using ETıcaretAPI.Application.Features.Products.Commands.Update;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandRequest:IRequest<DeletedProductCommandResponse>
    {
        public string Id { get; set; }
    }

}
