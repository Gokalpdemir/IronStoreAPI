using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Queries.GetById
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
       private readonly IProductReadRepository _productReadRepository;
        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
           Product product=  await  _productReadRepository.Table.Include(p=>p.ProductImageFiles).Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==Guid.Parse(request.Id));
            if (product!=null)
            {
                GetByIdProductQueryResponse response= new GetByIdProductQueryResponse()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryName=product.Category.Name,
                    ProductImageFiles=product.ProductImageFiles.Select(pif=>pif.Path).ToList(),
                };
                return response;
            }
            throw new Exception("hata");
        }
    }
}
