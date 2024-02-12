using Core.Entities;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            var context = new TContext();
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            var context = new TContext();
            context.Remove(entity);
            context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            var context = new TContext();
            return context.Set<TEntity>().SingleOrDefault(filter);
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            var context = new TContext();
            var _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            var sortColumn = "";
            try
            { sortColumn = _httpContextAccessor.HttpContext.Request.Headers["sortColumn"]; }
            catch { }

            Boolean.TryParse(_httpContextAccessor.HttpContext.Request.Headers["sortDescending"], out bool sortDescending);
            Boolean.TryParse(_httpContextAccessor.HttpContext.Request.Headers["stayInPager"], out bool stayInPager);
            int.TryParse(_httpContextAccessor.HttpContext.Request.Headers["pageNumber"].ToString(), out int pageNumber);
            int.TryParse(_httpContextAccessor.HttpContext.Request.Headers["pageSize"].ToString(), out int pageSize);

            var result = filter == null
                ? context.Set<TEntity>().AsQueryable()
                : context.Set<TEntity>().Where(filter).AsQueryable();

            if (!String.IsNullOrEmpty(sortColumn))
            {
                if (sortDescending)
                    result = result.OrderByDescendingGeneric(sortColumn);
                else
                    result = result.OrderByGeneric(sortColumn);
            }

            pageNumber = pageNumber < 1 ? 1 : pageNumber;

            if (pageSize > 0 && stayInPager)
            {
                var totalCount = result.Count();
                var totalPageCount = totalCount / pageSize;

                if (totalCount > 0 && totalCount % pageSize > 0)
                    totalPageCount++;

                if (totalPageCount < pageNumber)
                    pageNumber = 1;
            }

            if (pageSize >= 0)
                result = result.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return result;
        }

        public void Update(TEntity entity)
        {
            var context = new TContext();
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
