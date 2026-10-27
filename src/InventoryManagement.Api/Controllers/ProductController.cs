using InventoryManagement.Api.Contracts;
using InventoryManagement.Api.Contracts.Products;
using InventoryManagement.Application.Services;
using InventoryManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var product = await _productService.CreateProductAsync(request.Name, request.Price, request.StockQuantity, request.CategoryId);

        return CreatedAtAction(nameof(GetById),new{id= product.Id},new
        {
            product.Id,
            product.Name,
            product.Price,
            product.StockQuantity,
            product.CategoryId,
        });

    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products= await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product  = await _productService.GetProductByIdAsync(id);

        if(product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPatch("{id:int}/price")]
    public async Task<IActionResult> UpdatePrice(int id, UpdatePriceRequest request)
    {
        try
        {
            var updated = await _productService.UpdatePrice(id, request.Price);

            if(!updated)
            {
                return NotFound();
            }
            return NoContent();
        }catch(ArgumentException ex)
        {
            return BadRequest(new{message=ex.Message});
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteProductAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}