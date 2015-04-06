namespace Repository.RavenDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Raven.Client;
    using Raven.Client.Indexes;

    public class SearchableRepository<T, TSearch, TReduceResult> : ISearchableRepository<T>
        where T : IPersistable
        where TSearch : AbstractIndexCreationTask<T, TReduceResult>, new() 
        where TReduceResult : ISearchReduceResult
    {
        private readonly IDocumentSession documentSession;

        public SearchableRepository(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public IList<T> SearchByAny(IEnumerable<string> searchParams)
        {
            var searchString = string.Join(" ", searchParams.ToArray());
            var result = this.documentSession.Query<TReduceResult, TSearch>()
                    .Search(x => x.SearchQuery, searchString)
                    .As<T>()
                    .ToList();

            return result;
        }

        public IList<T> SearchByAll(IEnumerable<string> searchParams)
        {
            var searchString = string.Join(" ", searchParams.ToArray());
            var query = this.documentSession.Query<TReduceResult, TSearch>();
            var result = searchString.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (q, term) =>
                     q.Search(x => x.SearchQuery, term, options: SearchOptions.And));

            return result.As<T>().ToList();
        }
    }
}
