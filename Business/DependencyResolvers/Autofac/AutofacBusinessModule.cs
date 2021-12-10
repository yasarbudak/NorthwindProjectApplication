using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

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

      var assembly = System.Reflection.Assembly.GetExecutingAssembly();

      builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
        .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
        {
          Selector = new AspectInterceptorSelector()
        }).SingleInstance();
    }
  }
}