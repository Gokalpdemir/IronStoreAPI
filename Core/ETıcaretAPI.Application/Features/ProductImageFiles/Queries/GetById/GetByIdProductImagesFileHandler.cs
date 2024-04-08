using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Queries.GetById
{
    public class GetByIdProductImagesFileHandler : IRequestHandler<GetByIdProductImagesFileRequest, List<GetByIdProductImagesFileResponse>>
    {
        private readonly IProductReadRepository _productReadrepository;
        readonly private IConfiguration _configuration;
        public GetByIdProductImagesFileHandler(IProductReadRepository productReadrepository, IConfiguration configuration)
        {
            _productReadrepository = productReadrepository;
            _configuration = configuration;
        }
        public async Task<List<GetByIdProductImagesFileResponse>> Handle(GetByIdProductImagesFileRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadrepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
           
           return product.ProductImageFiles.Select(p => new GetByIdProductImagesFileResponse 
           {
               FileName = p.FileName,
               Id = p.Id,
               Path = $"{_configuration["BaseUrl"]}/{p.Path}",
           }
           ).ToList();

            


        }
    }
}
