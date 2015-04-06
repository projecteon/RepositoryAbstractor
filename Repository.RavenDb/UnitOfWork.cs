namespace Repository.RavenDb
{
    using System;

    using Raven.Client;

    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly IDocumentSession documentSession;

        #endregion

        #region Constructors and Destructors
        
        public UnitOfWork(IDocumentSession session)
        {
            this.documentSession = session;
        }

        #endregion
        
        #region Public Methods and Operators

        public void Commit()
        {
            this.documentSession.SaveChanges();
        }

        public void Dispose()
        {
            this.documentSession.Dispose();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}