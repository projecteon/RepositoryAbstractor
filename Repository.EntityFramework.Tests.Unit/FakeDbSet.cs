using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.EntityFramework.Tests.Unit
{
	public class FakeDbSet<T> : IDbSet<T>
		where T : class
	{
		private readonly HashSet<T> _data;
		private readonly IQueryable _query;

		public FakeDbSet()
		{
			_data = new HashSet<T>();
			_query = _data.AsQueryable();
		}

		public virtual T Find(params object[] keyValues)
		{
			throw new NotImplementedException("Derive from FakeDbSet and override Find");
		}

		public T Add(T item)
		{
			_data.Add(item);
			return item;
		}

		public T Remove(T item)
		{
			_data.Remove(item);
			return item;
		}

		public T Attach(T item)
		{
			_data.Add(item);
			return item;
		}

		public T Create()
		{
			throw new NotImplementedException();
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			throw new NotImplementedException();
		}

		public ObservableCollection<T> Local
		{
			get { throw new NotImplementedException(); }
		}

		public void Detach(T item)
		{
			_data.Remove(item);
		}

		Type IQueryable.ElementType
		{
			get { return _query.ElementType; }
		}

		Expression IQueryable.Expression
		{
			get { return _query.Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return _query.Provider; }
		}


		public IEnumerator<T> GetEnumerator()
		{
			return _data.AsEnumerable().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
