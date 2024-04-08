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


        public CreateProductCommandHandler(IProductWriteRepository productWriterepository)
        {
            _productWriterepository = productWriterepository;

        }
        public async Task<CreatedProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriterepository.AddAsync(new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await _productWriterepository.SaveAsync();
            return new CreatedProductCommandResponse()
            {
                IsSuccess = true,
            };

        }
    }
}
