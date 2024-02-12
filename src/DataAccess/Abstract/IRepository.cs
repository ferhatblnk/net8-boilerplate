using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(Guid id);
        TEntity Get(Expression<Func<TEntity, bool>> filter);

        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
        IPageResult<IList<TEntity>> GetPagedList(Expression<Func<TEntity, bool>> filter = null);
        IPageResult<IQueryable<TEntity>> GetPagedQuery(Expression<Func<TEntity, bool>> filter = null);

        TEntity Insert(TEntity entity, bool customID = false);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> FromSql(string sql);

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }
    }
}