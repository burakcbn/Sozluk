using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Application.Interfaces.Repositories;
using Sozluk.Api.Domain.Models;
using Sozluk.Api.Domain.Models.Common;
using Sozluk.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        protected DbSet<TEntity> _entity => _context.Set<TEntity>();

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        #region Insert

        public int Add(TEntity entity)
        {
            _entity.Add(entity);
            return _context.SaveChanges();
        }

        public int Add(IEnumerable<TEntity> entities)
        {
            _entity.AddRange(entities);
            return _context.SaveChanges();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public int Update(TEntity entity)
        {
            _entity.Update(entity);
            return _context.SaveChanges();
        }

        #endregion

        #region Delete
        public int Delete(TEntity entity)
        {
            _entity.Remove(entity);
            return _context.SaveChanges();
        }

        public int Delete(Guid id)
        {
            var entity = _entity.Find(id);

            return Delete(entity);
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _entity.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _entity.FindAsync(id);
            return await DeleteAsync(entity);
        }

        public bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _context.RemoveRange(_entity.Where(predicate));
            return _context.SaveChanges() > 0;
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _context.RemoveRange(predicate);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region AddOrUpdate
        public int AddOrUpdate(TEntity entity)
        {
            if (!_entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);
            return _context.SaveChanges();
        }

        public Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!_entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
                _context.Update(entity);
            return _context.SaveChangesAsync();
        }
        #endregion


        #region Get
        public IQueryable<TEntity> AsQueryable() => _entity.AsQueryable();
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = AsQueryable();
            if (predicate == null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query.AsNoTracking();
            return query;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = AsQueryable();
            if (noTracking)
                query.AsNoTracking();

            query = ApplyIncludes(query, includes);

            return await query.FirstOrDefaultAsync(predicate);
        }



        public async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            IQueryable<TEntity> query = _entity.AsQueryable();

            if (noTracking)
                query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = await _entity.FindAsync(id);
            if (entity == null)
                return null;

            if (noTracking)
                _context.Entry(entity).State = EntityState.Detached;

            foreach (Expression<Func<TEntity, object>> include in includes)
                _context.Entry(entity).Reference(include).Load();

            return entity;
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entity;

            if (predicate != null)
                query = query.Where(predicate);

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (orderBy != null)
                query = orderBy(query);

            if (noTracking)
                query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entity;
            if (predicate != null)
                query = query.Where(predicate);
            query = ApplyIncludes(query, includes);

            if (noTracking)
                query.AsNoTracking();
            return await query.SingleOrDefaultAsync();
        }
        #endregion

        #region Bulk

        public async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;

            await _entity.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && !ids.Any())
            {
                await Task.CompletedTask;
            }
            _context.RemoveRange(_entity.Where(i => ids.Contains(i.Id)));
            await _context.SaveChangesAsync();
        }

        public Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }


        #endregion
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }


    }
}
