using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework.Contexts;
using Core.Settings.Concrete;
using Microsoft.Extensions.Options;

namespace DataAccess.Concrete.EntityFramework
{
    public partial class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private DbContext _context;
        public GenericRepository() { }
        public virtual IQueryable<TEntity> Table => Entities.AsQueryable();
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking().AsQueryable();
        public virtual TEntity Get(Guid id) => TableNoTracking.FirstOrDefault(x => x.RowGuid == id);
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter) => TableNoTracking.FirstOrDefault(filter);
        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null) => filter == null
            ? TableNoTracking.AsQueryable()
            : TableNoTracking.Where(filter).AsQueryable();
        public virtual IPageResult<IList<TEntity>> GetPagedList(Expression<Func<TEntity, bool>> filter = null)
        {
            var _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            var sortColumn = "";
            try
            {
                sortColumn = _httpContextAccessor.HttpContext.Request.Headers["sortColumn"];
                sortColumn ??= "CreatedAt";
            }
            catch { }

            var searchText = "";
            try
            { searchText = _httpContextAccessor.HttpContext.Request.Headers["searchText"]; }
            catch { }

            var searchKey = "";
            try
            { searchKey = _httpContextAccessor.HttpContext.Request.Headers["searchKey"]; }
            catch { }

            _ = Boolean.TryParse(_httpContextAccessor.HttpContext.Request.Headers["sortDescending"], out bool sortDescending);
            _ = Boolean.TryParse(_httpContextAccessor.HttpContext.Request.Headers["stayInPager"], out bool stayInPager);
            _ = int.TryParse(_httpContextAccessor.HttpContext.Request.Headers["pageNumber"], out int pageNumber);
            _ = int.TryParse(_httpContextAccessor.HttpContext.Request.Headers["pageSize"], out int pageSize);

            pageSize = pageSize == 0 ? 5 : pageSize;

            var result = GetList(filter);

            if (!String.IsNullOrEmpty(sortColumn))
            {
                if (sortDescending)
                    result = result.OrderByDescendingGeneric(sortColumn);
                else
                    result = result.OrderByGeneric(sortColumn);
            }

            if (!String.IsNullOrEmpty(searchKey) && !String.IsNullOrEmpty(searchText))
                result = result.ContainsGeneric(searchKey, searchText);

            pageNumber = pageNumber < 1 ? 1 : pageNumber;

            var totalCount = result.Count();
            int totalPageCount = pageSize > 0 ? totalCount / pageSize : 0;

            if (totalCount > 0 && totalCount % pageSize > 0)
                totalPageCount++;

            if (pageSize > 0 && stayInPager)
            {
                if (totalPageCount < pageNumber)
                    pageNumber = 1;
            }

            if (pageSize >= 0 && pageNumber > 0 && !(pageNumber == 0 && pageSize == 0))
                result = result.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PageResult<IList<TEntity>>(result.ToList(), totalPageCount, pageNumber, totalCount, pageSize);
        }
        public virtual IPageResult<IQueryable<TEntity>> GetPagedQuery(Expression<Func<TEntity, bool>> filter = null)
        {
            var _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            var sortColumn = "";
            try
            {
                sortColumn = _httpContextAccessor.HttpContext.Request.Headers["sortColumn"];
                sortColumn ??= "CreatedAt";
            }
            catch { }

            var searchText = "";
            try
            { searchText = _httpContextAccessor.HttpContext.Request.Headers["searchText"]; }
            catch { }

            var searchKey = "";
            try
            { searchKey = _httpContextAccessor.HttpContext.Request.Headers["searchKey"]; }
            catch { }

            _ = Boolean.TryParse(_httpContextAccessor.HttpContext.Request.Headers["sortDescending"], out bool sortDescending);
            _ = Boolean.TryParse(_httpContextAccessor.HttpContext.Request.Headers["stayInPager"], out bool stayInPager);
            _ = int.TryParse(_httpContextAccessor.HttpContext.Request.Headers["pageNumber"].ToString(), out int pageNumber);
            _ = int.TryParse(_httpContextAccessor.HttpContext.Request.Headers["pageSize"].ToString(), out int pageSize);

            pageSize = pageSize == 0 ? 5 : pageSize;

            var result = GetList(filter);

            if (!String.IsNullOrEmpty(sortColumn))
            {
                if (sortDescending)
                    result = result.OrderByDescendingGeneric(sortColumn);
                else
                    result = result.OrderByGeneric(sortColumn);
            }

            if (!String.IsNullOrEmpty(searchKey) && !String.IsNullOrEmpty(searchText))
                result = result.ContainsGeneric(searchKey, searchText);

            pageNumber = pageNumber < 1 ? 1 : pageNumber;

            var totalCount = result.Count();
            int totalPageCount = pageSize > 0 ? totalCount / pageSize : 0;

            if (totalCount > 0 && totalCount % pageSize > 0)
                totalPageCount++;

            if (pageSize > 0 && stayInPager)
            {
                if (totalPageCount < pageNumber)
                    pageNumber = 1;
            }

            if (pageSize >= 0 && pageNumber > 0 && !(pageNumber == 0 && pageSize == 0))
                result = result.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PageResult<IQueryable<TEntity>>(result, totalPageCount, pageNumber, totalCount, pageSize);
        }
        public virtual TEntity Insert(TEntity entity, bool customID = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                try
                { _context.ChangeTracker.Clear(); }
                catch { }

                Entities.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                throw;
            }
        }
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                try
                { _context.ChangeTracker.Clear(); }
                catch { }

                Entities.AddRange(entities);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                try
                { _context.ChangeTracker.Clear(); }
                catch { }

                Entities.Update(entity);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                try
                { _context.ChangeTracker.Clear(); }
                catch { }

                Entities.UpdateRange(entities);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                try
                { _context.ChangeTracker.Clear(); }
                catch { }

                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                try
                { _context.ChangeTracker.Clear(); }
                catch { }

                Entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<TEntity> FromSql(string sql)
        {
            return Entities.FromSqlRaw(sql).AsNoTracking();
        }
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                _context ??= ServiceTool.ServiceProvider.GetService<AppDataContext>();

                return _context.Set<TEntity>();
            }
        }
    }
}