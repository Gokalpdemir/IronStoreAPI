using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Orders.Queries.GetById
{
    public class GetByIdOrderQueryRequest:IRequest<GetByIdOrderQueryResponse>
    {
        public string Id { get; set; }
    }
}
