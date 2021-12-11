using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
  public class ProductManager : IProductService
  {
    //Cross Cutting Concerns
    IProductDal _productDal;
    ICategoryService _categoryService;

    public ProductManager(IProductDal productDal, ICategoryService categoryService)
    {
      _productDal = productDal;
      _categoryService = categoryService;
    }

    //rainbow table
    [SecuredOperation("admin, editor")]
    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(Product product)
    {
      IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.ProductId), CheckIfProductNameExist(product.ProductName), CheckIfCategoryLimitExceeded());

      if (result != null)
      {
        return result;
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

    [ValidationAspect(typeof(ProductValidator))]
    public IResult Update(Product product)
    {
      var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
      if (result >= 10)
      {
        return new ErrorResult(Messages.ProductCountOfCategoryError);
      }

      throw new NotImplementedException();
    }

    private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
    {
      //A category can contain up to 10 products.
      var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
      if (result >= 10)
      {
        return new ErrorResult(Messages.ProductCountOfCategoryError);
      }
      return new SuccessResult();
    }

    private IResult CheckIfProductNameExist(string productName)
    {
      var result = _productDal.GetAll(p => p.ProductName == productName).Any();
      if (result == true)
      {
        return new ErrorResult(Messages.ProductNameAlreadyExist);
      }
      return new SuccessResult();
    }

    private IResult CheckIfCategoryLimitExceeded()
    {
      var result = _categoryService.GetAll();
      if (result.Data.Count > 5)
      {
        return new ErrorResult(Messages.CategoryLimitExceeded);
      }
      return new SuccessResult();
    }
  }
}