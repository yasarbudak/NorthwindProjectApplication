using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
  public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
  {
    public void Add(TEntity entity)
    {
      //using is an IDisposable pattern implementation of C#
      //Bir nesne, newlendikten sonra ve tüm referansları düşene kadar bellekte kalır. İşi biten nesneler, GarbageCollector tarafından belirli aralıklarla bellekten silinir.
      //using içinde oluşturulan nesneler ise, using bittiğinde GarbageCollector tarafından bellekten silinir. Performans için nesneler new'lenmek yerine using içinde kullanılmalıdır.
      using (TContext context = new TContext())
      {
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Added;
        context.SaveChanges();
      }
    }

    public void Delete(TEntity entity)
    {
      using (TContext context = new TContext())
      {
        var deletedEntity = context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        context.SaveChanges();
      }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
      using (TContext context = new TContext())
      {
        return context.Set<TEntity>().SingleOrDefault(filter);
      }
    } 

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
      using (TContext context = new TContext())
      {
        return filter == null
          ? context.Set<TEntity>().ToList()
          : context.Set<TEntity>().Where(filter).ToList();
      }
    }

    public void Update(TEntity entity)
    {
      using (TContext context = new TContext())
      {
        var updatedEntity = context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        context.SaveChanges();
      }
    }
  }
}