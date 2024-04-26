using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.ProductImageFiles.Commands.SelectShowCase
{
    public class SelectShowCaseCommandRequest:IRequest<SelectedShowCaseCommandResponse>
    {
        public string ImageId { get; set; }
        public string ProductId { get; set; }
    }
}
