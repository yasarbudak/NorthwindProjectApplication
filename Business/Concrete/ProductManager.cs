using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
  public class ProductManager : IProductService
  {
    IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
      _productDal = productDal;
    }

    public List<Product> GetAll() => _productDal.GetAll();
  }
}
