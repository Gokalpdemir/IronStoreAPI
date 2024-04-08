using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Commands.Delete
{
    public class DeleteProductImageCommandRequest:IRequest<DeletedProductImageCommandResponse>
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }

        public DeleteProductImageCommandRequest(string productId,string imageId)
        {
            ProductId = productId;
            ImageId = imageId;
        }

    }
}
