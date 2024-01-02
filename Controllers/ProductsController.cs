using CRUDWebAPI.Entities;
using CRUDWebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CRUDWebAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IProductService productService)
        {
            this.productService = productService;
            _logger = logger;
        }

        [HttpGet("getproductlist")]
        public async Task<List<Product>> GetProductListAsync()
        {
            try
            {
                return await productService.GetProductListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "IProductService.GetProductListAsync encountered an exception.");
                throw;
            }
        }

        [HttpGet("getproductbyid")]
        public async Task<IEnumerable<Product>> GetProductByIdAsync(int Id)
        {
            try
            {
                var response = await productService.GetProductByIdAsync(Id);

                if(response == null)
                {
                    return null;
                }

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "IProductService.GetProductByIdAsync encountered an exception.");
                throw;
            }
        }

        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            if(product == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await productService.AddProductAsync(product);

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "IProductService.AddProductAsync encountered an exception.");
                throw;
            }
        }

        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                var result =  await productService.UpdateProductAsync(product);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "IProductService.UpdateProductAsync encountered an exception.");
                throw;
            }
        }

        [HttpDelete("deleteproduct")]
        public async Task<int> DeleteProductAsync(int Id)
        {
            try
            {
                var response = await productService.DeleteProductAsync(Id);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "IProductService.DeleteProductAsync encountered an exception.");
                throw;
            }
        }
    }
}


