using System;
using System.Data;
using NHibernate;

namespace Repository.NHibernate
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ITransaction transaction;
		public ISession Session { get; private set; }

		public UnitOfWork(ISession session)
		{
			Session = session;
			if (Session.SessionFactory.IsClosed)
			{
				Session = Session.SessionFactory.OpenSession();
			}

			Session.FlushMode = FlushMode.Auto;
			transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
		}

		public void Dispose()
		{
			if (!Session.SessionFactory.IsClosed)
				Session.SessionFactory.Close();
		}

		public void Rollback()
		{
			if (transaction.IsActive)
			{
				transaction.Rollback();
			}
		}

		public void Commit()
		{
			if (!transaction.IsActive)
			{
				throw new InvalidOperationException("No active transation");
			}
			transaction.Commit();
		}
	}
}
