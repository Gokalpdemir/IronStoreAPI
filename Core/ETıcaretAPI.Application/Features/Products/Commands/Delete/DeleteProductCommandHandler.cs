using ETıcaretAPI.Application.Features.Products.Commands.Update;
using ETıcaretAPI.Application.Repositories;
using MediatR;

namespace ETıcaretAPI.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest,DeletedProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeletedProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
             await _productWriteRepository.RemoveAsync(request.Id);
             await _productWriteRepository.SaveAsync();
            return new DeletedProductCommandResponse()
            {
                IsSuccess = true,
            };
        }
    }
}
