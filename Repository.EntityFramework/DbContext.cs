using System.Data.Entity;

namespace Repository.EntityFramework
{
	public class DbContext : System.Data.Entity.DbContext, IDbContext
	{
		public IDbSet<T> DbSet<T>() where T : class
		{
			return Set<T>();
		}

		public void SetModified<T>(T entity) where T : class
		{
			this.Entry(entity).State = EntityState.Modified;
		}
	}
}
