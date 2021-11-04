using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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
      return new DataResult<List<Product>>(_productDal.GetAll(), true,"The products are listed.");
    }

    public List<Product> GetAllByCategoryId(int id) => _productDal.GetAll(p => p.CategoryId == id);

    public List<Product> GetByUnitPrice(decimal minPrice, decimal maxPrice) => _productDal.GetAll(p => p.UnitPrice <= minPrice && p.UnitPrice >= maxPrice);

    public Product GetById(int productId) => _productDal.Get(p => p.ProductId == productId);

    public List<ProductDetailDTO> GetProductDetails() => _productDal.GetProductDetails();
  }
}