using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
  public static class Messages
  {
    public static string ProductAdded = "The product added.";
    public static string ProductNameIsInvalid = "The product name is invalid.";
    public static string MaintenanceTime = "The system is in maintenance";
    public static string ProductsListed = "The products are listed.";
    public static string ProductCountOfCategoryError = "A category can contain up to 10 products.";
    public static string ProductNameAlreadyExist = "There cannot be two products with the same name.";
    internal static string CategoryLimitExceeded = "Category limit exceeded.";
  }
}