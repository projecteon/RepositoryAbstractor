using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Repository.EntityFramework
{
	public interface IDbContext
	{
		IDbSet<T> DbSet<T>() where T : class;
		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
		void SetModified<T>(T entity) where T : class;
		int SaveChanges();
		void Dispose();
	}
}
