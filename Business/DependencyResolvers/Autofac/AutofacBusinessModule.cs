using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
  public class AutofacBusinessModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
      builder.RegisterType<EFProductDal>().As<IProductDal>().SingleInstance();

      builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
      builder.RegisterType<EFCategoryDal>().As<ICategoryDal>().SingleInstance();

      builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
      builder.RegisterType<EFUserDal>().As<IUserDal>().SingleInstance();

      builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
      builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

      var assembly = System.Reflection.Assembly.GetExecutingAssembly();

      builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
        .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
        {
          Selector = new AspectInterceptorSelector()
        }).SingleInstance();
    }
  }
}