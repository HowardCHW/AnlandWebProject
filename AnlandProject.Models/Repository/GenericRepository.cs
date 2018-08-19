using AnlandProject.Models.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnlandProject.Models.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
          where TEntity : class
    {
        protected static Logger modelLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 資料庫作業類別
        /// </summary>
        private DbContext _context
        {
            get;
            set;
        }

        /// <summary>
        /// 是否處於交易模式 (整批執行)
        /// </summary>
        private bool _useTransaction { set; get; } = false;

        public GenericRepository()
            : this(new anlandEntities())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
            this._context.Database.Log = s => modelLogger.Info(s);
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = new DbContext(context, true);
            this._context.Database.Log = s => modelLogger.Info(s);
        }

        public int Create(TEntity instance)
        {
            int resultRows = 0;
            if (instance == null)
            {
                modelLogger.Error("GenericRepository Create", new ArgumentNullException("instance"));
            }
            else
            {
                _context.Set<TEntity>().Add(instance);
                resultRows = _context.SaveChanges();
            }
            return resultRows;
        }

        public int Update(TEntity instance)
        {
            int resultRows = 0;
            if (instance == null)
            {
                modelLogger.Error("GenericRepository Update", new ArgumentNullException("instance"));
            }
            else
            {
                _context.Entry<TEntity>(instance).State = EntityState.Modified;
                resultRows = _context.SaveChanges();
            }
            return resultRows;
        }

        public int Delete(TEntity instance)
        {
            int resultRows = 0;
            if (instance == null)
            {
                modelLogger.Error("GenericRepository Delete", new ArgumentNullException("instance"));
            }
            else
            {
                _context.Entry<TEntity>(instance).State = EntityState.Deleted;
                resultRows = _context.SaveChanges();
            }
            return resultRows;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedEnumerable<TEntity>> orderPredicate = null, params string[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (string includeProperty in includeProperties)
                query = query.Include(includeProperty);

            if (orderPredicate != null)
            {
                return orderPredicate(query).AsQueryable();
            }
            else
            {
                return query;
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public int GetCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return _context.Set<TEntity>().ToList().Count;
            }
            else
            {
                return _context.Set<TEntity>().Where(predicate).ToList().Count;
            }
        }

        public bool IsAny(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        /// <summary>
        /// 回收資源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 回收內部資源
        /// </summary>
        /// <param name="disposing">是否回收內部資源</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

    }
}
