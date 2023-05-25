namespace store.Services;

using store.Models;
using store.DTOs;
using store.Services;

public interface IProductService : ICrudService<Product, ProductDTO, ProductFilter, ProductUpdateDTO>
{

}