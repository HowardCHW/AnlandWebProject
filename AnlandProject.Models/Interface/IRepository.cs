using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Models.Interface
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        int Create(TEntity instance);

        int Update(TEntity instance);

        int Delete(TEntity instance);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>> orderPredicate = null, params string[] includeProperties);

        IQueryable<TEntity> GetAll();

        int GetCount(Expression<Func<TEntity, bool>> predicate);

        bool IsAny(Expression<Func<TEntity, bool>> predicate);
    }
}
