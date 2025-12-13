using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySaaS.Application.DTOs.Products;
using MySaaS.Application.Interfaces.Products.Products;

namespace MySaaS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService productService)
        {
            _service = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            var result = await _service.AddAsync(createProductDTO);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO updateProductDTO)
        {
            await _service.UpdateAsync(updateProductDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _service.RemoveAsync(id);
            return Ok();
        }
    }
}
