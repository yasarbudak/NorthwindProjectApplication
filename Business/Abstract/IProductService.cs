using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
  public interface IProductService
  {
    IDataResult<List<Product>> GetAll();
    IDataResult<Product> GetAllByCategoryId(int id);
    IDataResult<Product> GetByUnitPrice(decimal minPrice, decimal maxPrice);
    IDataResult<ProductDetailDTO> GetProductDetails();
    IDataResult<Product> GetById(int productId);
    IResult Add(Product product);
  }
}