namespace Repository.RavenDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Raven.Client;

    public class Repository<T> : IRepository<T> where T : class, IPersistable
    {
        private readonly IDocumentSession documentSession;

        public Repository(IDocumentSession  documentSession)
        {
            this.documentSession = documentSession;
        }

        public void Add(T entity)
        {
            documentSession.Store(entity);
        }

        public void Delete(T entity)
        {
            documentSession.Delete(entity);
        }

        public bool Update(T entity)
        {
            documentSession.Store(entity);
            return true;
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> predicate)
        {
            return documentSession.Query<T>().Where(predicate);
        }

        public IList<T> GetAll(int pageIndex, int pageSize)
        {
            return documentSession.Query<T>().Skip(pageIndex).Take(pageSize).ToList();
        }
        
        public IList<T> GetAll()
        {
            return documentSession.Query<T>().ToList();
        }

        public T SingleBy(Expression<Func<T, bool>> predicate)
        {
            return documentSession.Query<T>().SingleOrDefault(predicate);
        }

        public T SingleBy(string id)
        {
            return documentSession.Load<T>(id);
        }
    }
}
