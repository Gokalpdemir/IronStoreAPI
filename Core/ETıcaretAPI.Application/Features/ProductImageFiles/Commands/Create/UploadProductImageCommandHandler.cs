using ETıcaretAPI.Application.Abstractions.Storage;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using MediatR;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Commands.Create
{
    public class UploadProductImageCommandHandler:IRequestHandler<UploadProductImageCommandRequest, UploadedProductImageCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        readonly private IStorageService _storageService;

        public UploadProductImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
        }
        public async Task<UploadedProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _storageService.UploadAsync("resource/photo-images", request.FormCollection);
            Product product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productImageFileWriteRepository.AddRangeAsync(data.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new UploadedProductImageCommandResponse()
            {
                IsSuccess = true,
            };
        }
    }
}
