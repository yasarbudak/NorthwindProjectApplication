using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
    public static string CategoryLimitExceeded = "Category limit exceeded.";
    public static string AuthorizationDenied = "Access Denied.";
    public static string UserRegistered = "The user is registered in the system.  ";
    public static string UserNotFound = "User not found.";
    public static string PasswordError = "User's password is wrong";
    public static string SuccessfulLogin = "Login successful";
    public static string AccessTokenCreated = "Access token created.";
    public static string UserAlreadyExist = "User already exist";
  }
}