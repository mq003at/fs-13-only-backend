namespace store.Controllers;

using store.Services;
using store.Models;
using store.DTOs;
using store.Controllers;

using Microsoft.AspNetCore.Mvc;

public class CategorieController : CrudController<Category, CategoryDTO, CategoryFilter, CategoryUpdateDTO>
{
    private readonly ICategoryService _CategoryService;

    public CategorieController(ICategoryService service)
        : base(service)
    {
        _CategoryService = service;
    }

    

}
