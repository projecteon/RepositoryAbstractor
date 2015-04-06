using System;
using System.Linq;
using Raven.Client;

namespace Repository.RavenDb.Tests.Unit
{
    public class FakeRavenQueryProvider : IQueryProvider
    {
        private readonly IQueryable source;
        private readonly RavenQueryStatistics stats;

        public FakeRavenQueryProvider(IQueryable source, RavenQueryStatistics stats = null)
        {
            this.source = source;
            this.stats = stats;
        }

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            return new FakeRavenQueryable<TElement>(source.Provider.CreateQuery<TElement>(expression), stats);
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {

            var type = typeof(FakeRavenQueryable<>).MakeGenericType(expression.Type);
            return (IQueryable)Activator.CreateInstance(type, source.Provider.CreateQuery(expression), stats);
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            return source.Provider.Execute<TResult>(expression);
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            return source.Provider.Execute(expression);
        }
    }
}