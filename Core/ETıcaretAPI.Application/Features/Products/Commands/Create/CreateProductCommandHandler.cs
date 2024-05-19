using ETıcaretAPI.Application.Abstractions.Hubs;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreatedProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriterepository;
        private readonly IProductHubService _productHubService;


        public CreateProductCommandHandler(IProductWriteRepository productWriterepository, IProductHubService productHubService)
        {
            _productWriterepository = productWriterepository;
            _productHubService = productHubService;
        }
        public async Task<CreatedProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriterepository.AddAsync(new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId=Guid.Parse(request.CategoryId),
            });
            await _productWriterepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklendi");
            return new CreatedProductCommandResponse()
            {
                IsSuccess = true,
            };

        }
    }
}
