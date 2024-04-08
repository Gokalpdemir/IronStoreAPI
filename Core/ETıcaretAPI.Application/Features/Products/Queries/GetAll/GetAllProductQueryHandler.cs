using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Application.RequestParametters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Commands.Queries.GetAll
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadrepository;
        public GetAllProductQueryHandler(IProductReadRepository productReadrepository)
        {

            _productReadrepository = productReadrepository;

        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadrepository.GetAll(false).Count();
            var products = _productReadrepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return new GetAllProductQueryResponse()
            {
                Products = products,
                TotalCount = totalCount

            };


        }
    }
}
