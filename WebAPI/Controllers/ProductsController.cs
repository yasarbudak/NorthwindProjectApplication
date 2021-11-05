using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]//Attribute //For java Annonation
  public class ProductsController : ControllerBase
  {
    //Loosely coupled
    IProductService _productService;

    public ProductsController(IProductService productService)
    {
      _productService = productService;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
      //Swagger
      //Dependency chain!
      //IProductService productService = new ProductManager(new EFProductDal());
      var result = _productService.GetAll();
      if (result.Success)
        return Ok(result);
      else
        return BadRequest(result);
    }

    [HttpGet("getbyid")]
    public IActionResult GetById(int productId)
    {
      var result = _productService.GetById(productId);
      if (result.Success)
        return Ok(result);
      else
        return BadRequest(result);
    }

    [HttpPost("add")]
    public IActionResult Add(Product product)
    {
      var result = _productService.Add(product);
      if (result.Success)
        return Ok(result);
      else
        return BadRequest(result);
    }
  }
}