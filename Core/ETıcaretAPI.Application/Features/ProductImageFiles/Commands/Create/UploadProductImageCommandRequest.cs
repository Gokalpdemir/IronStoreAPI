using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Commands.Create
{
    public class UploadProductImageCommandRequest:IRequest<UploadedProductImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection FormCollection { get; set; }
         
    }
}
