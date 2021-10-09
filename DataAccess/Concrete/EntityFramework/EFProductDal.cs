using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
  public class EFProductDal : IProductDal
  {
    public void Add(Product entity)
    {
      //using is an IDisposable pattern implementation of C#
      //Bir nesne, newlendikten sonra ve tüm referansları düşene kadar bellekte kalır. İşi biten nesneler, GarbageCollector tarafından belirli aralıklarla bellekten silinir.
      //using içinde oluşturulan nesneler ise, using bittiğinde GarbageCollector tarafından bellekten silinir. Performans için nesneler new'lenmek yerine using içinde kullanılmalıdır.
      using (NorthwindContext context = new NorthwindContext())
      {
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Added;
        context.SaveChanges();
      }
    }

    public void Delete(Product entity)
    {
      using (NorthwindContext context = new NorthwindContext())
      {
        var deletedEntity = context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        context.SaveChanges();
      }
    }

    public Product Get(Expression<Func<Product, bool>> filter)
    {
      using (NorthwindContext context = new NorthwindContext())
      {
        return context.Set<Product>().SingleOrDefault(filter);
      }
    }

    public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
    {
      using (NorthwindContext context = new NorthwindContext())
      {
        return filter == null
          ? context.Set<Product>().ToList()
          : context.Set<Product>().Where(filter).ToList();
      }
    }

    public List<Product> GetAllByCategory(int categoryId)
    {
      throw new NotImplementedException();
    }

    public void Update(Product entity)
    {
      using (NorthwindContext context = new NorthwindContext())
      {
        var updatedEntity = context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        context.SaveChanges();
      }
    }
  }
}
