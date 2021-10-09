using Business.Concrete;
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
      ProductManager productManager = new ProductManager(new EFProductDal());
      foreach (var item in productManager.GetAll())
      {
        Console.WriteLine(item.ProductName);
      }
      Console.Read();
    }
  }
}
