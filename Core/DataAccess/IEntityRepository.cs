using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess
{
  //generic constraint ile, bu interface'i implemente edecek classlar için tip güvenliği sağlandı.
  //class: sadece referans tiplerin parametre geçilmesine olanak tanındı.
  //IEntity: IEntity olabilir veya IEntity implemente eden bir nesne olabilir.
  //new(): new'lenebilen referans tipler olabilir.
  public interface IEntityRepository<T> where T : class, IEntity, new()
  {
    List<T> GetAll(Expression<Func<T, bool>> filter = null);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
}