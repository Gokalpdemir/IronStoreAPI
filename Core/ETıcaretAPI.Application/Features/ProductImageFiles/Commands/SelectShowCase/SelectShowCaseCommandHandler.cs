using ETıcaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Commands.SelectShowCase
{
    public class SelectShowCaseCommandHandler : IRequestHandler<SelectShowCaseCommandRequest, SelectedShowCaseCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public SelectShowCaseCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<SelectedShowCaseCommandResponse> Handle(SelectShowCaseCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Products)
                 .SelectMany(p => p.Products, (pif, p) => new
                 {
                     pif,
                     p
                 });
            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);
            if (data != null)
                data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
            if (image != null)
                image.pif.Showcase = true;
            await _productImageFileWriteRepository.SaveAsync();

            return new SelectedShowCaseCommandResponse();
        }
    }
}
