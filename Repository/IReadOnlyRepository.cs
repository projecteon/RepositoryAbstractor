namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IReadOnlyRepository<T> where T : class
    {
        IQueryable<T> FilterBy(Expression<Func<T, bool>> predicate);

        IList<T> GetAll();
        
        IList<T> GetAll(int pageIndex, int pageSize);

        T SingleBy(Expression<Func<T, bool>> predicate);
    }
}