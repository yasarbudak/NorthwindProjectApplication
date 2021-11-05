using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
  public class ProductManager : IProductService
  {
    IProductDal _productDal;

    public ProductManager(IProductDal productDal) => _productDal = productDal;
    public IResult Add(Product product)
    {
      if (product.ProductName.Length < 2)
      {
        return new ErrorResult(Messages.ProductNameIsInvalid);
      }
      _productDal.Add(product);
      return new SuccessResult(Messages.ProductAdded);
    }
    public IDataResult<List<Product>> GetAll()
    {
      if (DateTime.Now.Hour == 21)
      {
        return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
      }
      return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
    }
    public IDataResult<List<Product>> GetAllByCategoryId(int id)
    {
      return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
    }
    public IDataResult<List<Product>> GetByUnitPrice(decimal minPrice, decimal maxPrice)
    {
      return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= minPrice && p.UnitPrice >= maxPrice));
    }
    public IDataResult<List<ProductDetailDTO>> GetProductDetails()
    {
      if (DateTime.Now.Hour == 18)
      {
        return new ErrorDataResult<List<ProductDetailDTO>>(Messages.MaintenanceTime);
      }
      return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());
    }
    public IDataResult<Product> GetById(int productId)
    {
      return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
    }
  }
}