using ETıcaretAPI.Application.Abstractions.Storage;
using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Application.RequestParametters;

using ETıcaretAPI.Application.ViewModels.Products;
using ETıcaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MediatR;

using ETıcaretAPI.Application.Features.Products.Commands.Queries.GetAll;
using ETıcaretAPI.Application.Features.Products.Commands.Create;
using ETıcaretAPI.Application.Features.Products.Queries.GetById;
using ETıcaretAPI.Application.Features.Products.Commands.Update;
using ETıcaretAPI.Application.Features.Products.Commands.Delete;
using ETıcaretAPI.Application.Features.ProductImageFiles.Commands.Create;
using ETıcaretAPI.Application.Features.ProductImageFiles.Commands.Delete;
using ETıcaretAPI.Application.Features.ProductImageFiles.Queries.GetById;
using Microsoft.AspNetCore.Authorization;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriterepository;
        readonly private IProductReadRepository _productReadrepository;
        readonly private IWebHostEnvironment _webHostEnvironment;
        readonly private IStorageService _storageService;
        readonly private IFileWriteRepository _fileWriteRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly private IFileReadRepository _fileReadRepository;
        readonly private IProductImageFileReadRepository _productImageFileReadRepository;
        readonly private IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly private IConfiguration _configuration;


        readonly private IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadrepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriterepository = productWriteRepository;
            _productReadrepository = productReadrepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
             GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
           return Ok(response);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
          GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);            
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CreateProductCommandRequest createProductCommandRequest)
        {
          CreatedProductCommandResponse response=  await _mediator.Send(createProductCommandRequest);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProductCommandRequest updateProductCommandRequest)
        {
           UpdatedProductCommandResponse response= await _mediator.Send(updateProductCommandRequest);
            return Ok(response);            
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]DeleteProductCommandRequest deleteProductCommandRequest)
        {
           DeletedProductCommandResponse response= await _mediator.Send(deleteProductCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]string id)
        {
            UploadProductImageCommandRequest uploadProductImageCommandRequest = new UploadProductImageCommandRequest();
            uploadProductImageCommandRequest.Id = id;
            uploadProductImageCommandRequest.FormCollection = Request.Form.Files; 
          UploadedProductImageCommandResponse response= await _mediator.Send(uploadProductImageCommandRequest);
         return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async  Task<IActionResult> GetProductsImage(string id)
        {
            GetByIdProductImagesFileRequest getByIdProductImagesFileRequest = new GetByIdProductImagesFileRequest(id);
           List<GetByIdProductImagesFileResponse> response= await _mediator.Send(getByIdProductImagesFileRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{productId}")]
        public async Task<IActionResult> DeleteProductImage( [FromRoute]string productId,[FromQuery] string imageId) 
        {
            DeleteProductImageCommandRequest deleteProductImageCommandRequest= new DeleteProductImageCommandRequest(productId,imageId);   
          DeletedProductImageCommandResponse response = await _mediator.Send(deleteProductImageCommandRequest);
            return Ok(response);
        }
       
    }
}
