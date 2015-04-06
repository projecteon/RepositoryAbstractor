using System.Data.Entity;

namespace Repository.EntityFramework
{
	public interface IUnitOfWork : Repository.IUnitOfWork
	{
		IDbSet<T> Set<T>() where T : class;
	}
}
