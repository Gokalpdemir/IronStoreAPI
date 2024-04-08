using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Queries.GetById
{
    public class GetByIdProductImagesFileRequest:IRequest<List<GetByIdProductImagesFileResponse>>
    {
        public string Id { get; set; }
        public GetByIdProductImagesFileRequest(string id)
        {
            Id = id;
        }
    }
}
