using ETıcaretAPI.Application.Repositories;
using ETıcaretAPI.Application.ViewModels.Products;
using ETıcaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriterepository;
        readonly private IProductReadRepository _productReadrepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadrepository)
        {
            _productWriterepository = productWriteRepository;
            _productReadrepository = productReadrepository;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var result = await _productReadrepository.GetByIdAsync(id,false);
            return Ok(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productReadrepository.GetAll(false);

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(VM_Create_Product model)
        {
            await _productWriterepository.AddAsync(new Product
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriterepository.SaveAsync();
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(VM_Update_Product model)
        {

            Product product = await _productReadrepository.GetByIdAsync(model.Id);
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.Name = model.Name;
            await _productWriterepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
           await _productWriterepository.RemoveAsync(id);
           await _productWriterepository.SaveAsync();
            return Ok();
        }
    }
}
