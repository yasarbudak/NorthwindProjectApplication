using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
  //SOLID
  //Open Closed Principle. Yaptığın yazılıma yeni bir özellik ekliyorsan, mevcuttaki hiç bir koduna dokunamazsın.
  //Sistemi EntityFramework'e geçirdik, sadece o iş ile ilgili classı ekledik. Mevcut kodlara hiç dokunmadık.
  class Program
  {
    static void Main(string[] args)
    {
      //ProductTest();
      //CategoryTest();
      GetProductDetails();
    }

    private static void GetProductDetails()
    {
      ProductManager productManager = new ProductManager(new EFProductDal());
      var result = productManager.GetProductDetails();
      if (result.Success)
      {
        foreach (var item in productManager.GetProductDetails().Data)
        {
          Console.WriteLine($"{item.ProductName} / {item.CategoryName}");
        }
      }
      else
      {
        Console.WriteLine(result.Message);
      }
      Console.Read();
    }

    private static void CategoryTest()
    {
      CategoryManager categoryManager = new CategoryManager(new EFCategoryDal());
      foreach (var category in categoryManager.GetAll())
      {
        Console.WriteLine($"{category.CategoryName}");
      }
    }

    private static void ProductTest()
    {
      ProductManager productManager = new ProductManager(new EFProductDal());
      foreach (var item in productManager.GetAll().Data)
      {
        Console.WriteLine($"{item.ProductName}");
      }
      Console.Read();
    }
  }
}