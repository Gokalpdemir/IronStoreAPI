using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Commands.Delete
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommandRequest, DeletedProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadrepository;
        private readonly IProductWriteRepository _productWriterepository;
        public DeleteProductImageCommandHandler(IProductReadRepository productReadrepository, IProductWriteRepository productWriterepository)
        {
            _productReadrepository = productReadrepository;
            _productWriterepository = productWriterepository;
        }
        public async Task<DeletedProductImageCommandResponse> Handle(DeleteProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await _productReadrepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.ProductId));

            ProductImageFile? productImageFile = product?.ProductImageFiles.FirstOrDefault(i => i.Id == Guid.Parse(request.ImageId));
            if (productImageFile != null)
                product?.ProductImageFiles.Remove(productImageFile);

            await _productWriterepository.SaveAsync();
            return new DeletedProductImageCommandResponse()
            {
                IsSuccess = true,
            };
        }
    }
}
