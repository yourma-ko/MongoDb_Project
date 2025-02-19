using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts([FromQuery] string? category)
        {
            var products = await productService.GetAllAsync();

            if (!string.IsNullOrEmpty(category))
            {
                products = await productService.GetByCategoryAsync(category);
            }

            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromServices] IProductService productService, Product product)
        {
            await productService.AddAsync(product);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromServices] IProductService productService, Product product)
        {
            await productService.UpdateAsync(product);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct([FromServices] IProductService productService, string id)
        {
            await productService.DeleteAsync(id);
            return Ok();
        }
        [HttpPost("many")]
        public async Task<ActionResult> AddMany([FromServices] IProductService productService, List<Product> products)
        {
            foreach(var product in products)
            {
                await productService.AddAsync(product);
            }
            return Ok();
        }
    }
}
