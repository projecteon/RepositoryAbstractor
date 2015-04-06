using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace Repository.NHibernate
{
	public class Repository<T> : IRepository<T>, IEnumerable where T : class, IPersistable
	{
		private readonly ISession session;

		public Repository(ISession session)
		{
			this.session = session;
		}

		public void Add(T entity)
		{
			session.Save(entity);
		}

		public void Delete(T entity)
		{
			session.Delete(entity);
		}

		public bool Update(T entity)
		{
			session.Update(entity);
			return true;
		}

		public IQueryable<T> AsQueryable()
		{
			return All().AsQueryable();
		}

		public T FindBy(Expression<Func<T, bool>> predicate)
		{
			return FilterBy(predicate).Single();
		}

		public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
		{
			return All().Where(expression).AsQueryable();
		}

		public IList<T> GetAll()
		{
			return GetAll(0, 0);
		}

		public T Single(Expression<Func<T, bool>> query)
		{
			return session.Query<T>().Where(query).Take(1).FirstOrDefault();
		}

		public IQueryable<T> All()
		{
			return session.Query<T>();
		}

		public Type ElementType
		{
			get { return session.Query<T>().ElementType; }
		}

		public Expression Expression
		{
			get { return session.Query<T>().Expression; }
		}

		public IQueryProvider Provider
		{
			get { return session.Query<T>().Provider; }
		}

		public IList<T> GetAll(int pageIndex, int pageSize)
		{
			var criteria = session.CreateCriteria(typeof(T));
			criteria.SetFirstResult(pageIndex * pageSize);
			if (pageSize > 0)
			{
				criteria.SetMaxResults(pageSize);
			}
			
			return criteria.List<T>();
		}

		public T SingleBy(Expression<Func<T, bool>> predicate)
		{
			return session.Query<T>().Where(predicate).Take(1).FirstOrDefault();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return session.Query<T>().GetEnumerator();
		}

		public T SingleBy(string id)
		{
			return session.Get<T>(id);
		}
	}
}
