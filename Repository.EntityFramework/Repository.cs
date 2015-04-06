using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.EntityFramework
{
	public class Repository<T> : IRepository<T>, IEnumerable where T : class, IPersistable
    {
        private readonly IDbSet<T> dbSet;
        private readonly UnitOfWork unitOfWork;

        public Repository(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            dbSet = unitOfWork.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return dbSet;
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return FilterBy(predicate).Single();
        }

        public IList<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IList<T> GetAll(int pageIndex, int pageSize)
        {
			return dbSet.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public T SingleBy(Expression<Func<T, bool>> predicate)
        {
			return dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> predicate)
        {
            var query = dbSet.Where(predicate);
            return query;
        }

        public Type ElementType
        {
            get { return dbSet.ElementType; }
        }

        public Expression Expression
        {
            get { return dbSet.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return dbSet.Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dbSet.GetEnumerator();
        }

        public T Single(Expression<Func<T, bool>> where)
        {
            var query = dbSet.Single(where);
            return query;
        }

        public bool Update(T entity)
        {
            unitOfWork.Context.SetModified(entity);
            return true;
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Create()
        {
            return dbSet.Create();
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T SingleBy(string id)
        {
            return dbSet.First(x => x.Id == id);
        }
    }
}
