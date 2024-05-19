using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Application.RequestParametters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetAllProductQueryHandler> _logger;
        public GetAllProductQueryHandler(IProductReadRepository productReadrepository, ILogger<GetAllProductQueryHandler> logger)
        {

            _productReadrepository = productReadrepository;
            _logger = logger;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
           
            
            var totalProductCount = _productReadrepository.GetAll(false).Count();
            var products = _productReadrepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Include(p=>p.ProductImageFiles
            ).Include(p=>p.Category).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate,
                p.ProductImageFiles,
              categoryName=  p.Category.Name
            }).ToList();

            return new GetAllProductQueryResponse()
            {
                Products = products,
                TotalProductCount = totalProductCount

            };


        }
    }
}
