namespace store.Controllers;

using store.Services;
using store.Models;
using store.DTOs;
using store.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class ProductController
    : CrudController<Product, ProductDTO, ProductFilter, ProductUpdateDTO>
{
    private readonly IProductService _productService;

    public ProductController(IProductService service)
        : base(service)
    {
        _productService = service;
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public override async Task<IActionResult> Create(ProductDTO request)
    {
        return await base.Create(request);
    }

    [Authorize(Policy = "Admin")]
    [HttpPut("{id:int}")]
    public override async Task<ActionResult<Product?>> Update(int id, ProductUpdateDTO request) =>
        await base.Update(id, request);

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id}")]
    public override async Task<ActionResult> Delete(int id)
    {
        return await base.Delete(id);
    }

    [HttpGet("test")]
    public async Task<ActionResult> Get()
    {
        await Task.CompletedTask;
        return Ok(new { Message = "Test completed!" });
    }
}
