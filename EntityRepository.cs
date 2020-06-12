using FastMed.DAL.Context;
using FastMed.DAL.Entity.Entities.Bases;
using FastMed.Infrastructure.Helpers.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastMed.DAL.Repositories.GenericRepository
{
    public class EntityRepository : IEntityRepository
    {
        private readonly IFastMedDbContext _context;

        public EntityRepository(IFastMedDbContext context)
        {
            _context = context;
        }

        #region Async Read Part

        public async Task<IEnumerable<T>> GetAllAsync<T>(bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync<T>(bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, include) => current.Include(include));
            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByPagingAsNoTrackingAsync<T>(bool includeDeleted = false, int page = 1,
            int pageSize = 10,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Skip(page * pageSize).Take(pageSize).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, include) => current.Include(include));
            return await set.ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Where(x => x.Id == id);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsNoTrackingAsync<T>(int id, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Where(x => x.Id == id).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FilterAsNoTrackingAsync<T>(Expression<Func<T, bool>> query,
            bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.ReaderSet<T>(includeDeleted).Where(query).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.ToListAsync();
        }

        #endregion

        #region Sync Read Part

        public IQueryable<T> GetAll<T>(bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> GetAllAsNoTracking<T>(bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> GetByPagingAsNoTracking<T>(int page = 1, int pageSize = 10, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Skip(page * pageSize).Take(pageSize)
                .AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, include) => current.Include(include));
            return set;
        }

        public T GetById<T>(int id, bool includeDeleted = false, params Expression<Func<T, object>>[] includeExpression)
            where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Where(entity => entity.Id == id);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set.FirstOrDefault();
        }

        public T GetByIdAsNoTracking<T>(int id, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Where(entity => entity.Id == id).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set.FirstOrDefault();
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> query, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.ReaderSet<T>(includeDeleted).Where(query);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> FilterAsNoTracking<T>(Expression<Func<T, bool>> query, bool includeDeleted = false,
            params Expression<Func<T, object>>[] includeExpression) where T : class, IEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.ReaderSet<T>(includeDeleted).Where(query).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> FilterWithQuery<T>(Expression<Func<T, bool>> query,
            Func<IQueryable<T>, IQueryable<T>> includeMembers, bool includeDeleted = false) where T : class, IEntity
        {
            var set = _context.ReaderSet<T>(includeDeleted).Where(query);
            var result = includeMembers(set);
            return result;
        }

        #endregion

        #region CUD Part

        public T Create<T>(T entity) where T : class, IEntity
        {
            _context.WriterSet<T>().Add(entity);
            return entity;
        }

        public bool CreateRange<T>(IEnumerable<T> entity) where T : class, IEntity
        {
            _context.WriterSet<T>().AddRange(entity);
            return true;
        }

        public bool Remove<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
                return true;
            entity.IsDeleted = true;
            _context.WriterSet<T>().Update(entity);
            return true;
        }

        public bool HardRemove<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
                return true;
            _context.WriterSet<T>().Remove(entity);
            return true;
        }

        public async Task<bool> RemoveRange<T>(IList<int> ids) where T : class, IEntity
        {
            var entityToRemove = await Filter<T>(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var variable in entityToRemove)
            {
                if (variable == null)
                    continue;
                variable.IsDeleted = true;
            }
            _context.WriterSet<T>().UpdateRange(entityToRemove);
            return true;
        }

        public async Task<bool> HardRemoveRange<T>(IList<int> ids) where T : class, IEntity
        {
            var entityToRemove = await Filter<T>(x => ids.Contains(x.Id)).ToListAsync();
            _context.WriterSet<T>().RemoveRange(entityToRemove);
            return true;
        }

        public bool Update<T>(T entity) where T : class, IEntity
        {
            if (entity == null)
                return false;
            _context.WriterSet<T>().Update(entity);
            return true;
        }

        #endregion

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IFastMedDbContext GetContext()
        {
            return _context;
        }
    }
}