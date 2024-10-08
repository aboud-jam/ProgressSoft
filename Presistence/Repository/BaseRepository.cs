using Core.Entity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Presistence.context;
using Presistence.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repository;

public class BaseRepository<TDbContext, T> : IRepository<TDbContext, T> where TDbContext : DbContext
                                                                            where T : class
{
    protected TDbContext _context;
    public BaseRepository(TDbContext context)
    {
        _context = context;
    }
    public void Add(T entity)
    {
        if (typeof(T).GetInterface(nameof(ICreationInfo)) != null)
        {
            var creationInfo = (ICreationInfo)entity;
            creationInfo.CreatedOn = DateTime.UtcNow;
        }
        _context.Add(entity);
    }

    

    public bool Delete(T entity, bool isHardDelete = false)
    {
        bool isDeleted = false;

        if (typeof(T).GetInterface(nameof(ISoftDeletion)) != null && !isHardDelete)
        {
            var deletionInfo = (ISoftDeletion)entity;
            deletionInfo.DeletedOn = DateTime.UtcNow;
            deletionInfo.IsDeleted = true;
            _context.Update(entity);
        }
        else
        {
            var attached = _context.Remove(entity);
        }
        isDeleted = true;
        return isDeleted;
    }




    public async Task<T> GetSingle(
    Expression<Func<T, bool>> predicate = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
    bool disableTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();

        // Apply tracking options
        if (disableTracking) query = query.AsNoTracking();

        // Include related entities if any
        if (include != null) query = include(query);

        // Apply filtering criteria
        if (predicate != null) query = query.Where(predicate);

        // Return the first or default value found
        return await query.FirstOrDefaultAsync();
    }

    /*public async Task<TResult> GetSingle<TResult>(Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool disableTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).Select(selector).FirstOrDefaultAsync();
        else return await query.Select(selector).FirstOrDefaultAsync();
    }*/






    public async Task<bool> Any(Expression<Func<T, bool>> predicate = null)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }

    public async Task<IEnumerable<TResult>> GetList<TResult>(Expression<Func<T, TResult>> selector,
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        bool isAsc = false, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool disableTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (typeof(T).GetInterface(nameof(ISoftDeletion)) != null) query = query.Where(item => !((ISoftDeletion)item).IsDeleted);
        
        if (orderBy != null) query = orderBy(query);
        return await query.Select(selector).ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

public class BaseRepository<TType> : BaseRepository<AppDbContext, TType>, IRepository<TType> where TType : class
{
    public BaseRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}