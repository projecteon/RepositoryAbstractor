using System;
using System.Data.Entity;

namespace Repository.EntityFramework
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly IDbContext context;

		public UnitOfWork(IDbContext context)
		{
			this.context = context;
		}

		public void Dispose()
		{
			context.Dispose();
		}

		public IDbSet<T> Set<T>() where T : class
		{
			return context.DbSet<T>();
		}

		public void Commit()
		{
			context.SaveChanges();
		}

		public void Rollback()
		{
			throw new NotImplementedException();
		}

		public IDbContext Context { get { return context; } }
	}
}
