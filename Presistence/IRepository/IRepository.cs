using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Presistence.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.IRepository;

public interface IRepository<TDbContext, TType> where TDbContext : DbContext
                                                    where TType : class
{
    void Add(TType entity);

   
    bool Delete(TType entity, bool isHardDelete = false);


    Task<TType> GetSingle(
    Expression<Func<TType, bool>> predicate = null,
    Func<IQueryable<TType>, IIncludableQueryable<TType, object>> include = null,
    bool disableTracking = false);




    Task<IEnumerable<TResult>> GetList<TResult>(Expression<Func<TType, TResult>> selector,
        Expression<Func<TType, bool>> predicate = null,
        Func<IQueryable<TType>, IOrderedQueryable<TType>> orderBy = null,
        bool isAsc = false,
        Func<IQueryable<TType>, IIncludableQueryable<TType, object>> include = null,
        bool disableTracking = false);

    

    Task<bool> Any(Expression<Func<TType, bool>> predicate = null);
    Task<int> SaveChangesAsync();
}

public interface IRepository<TType> : IRepository<AppDbContext, TType> where TType : class
{

}
