using System.Linq.Expressions;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using Raven.Client.Spatial;
using Raven.Json.Linq;

namespace Repository.RavenDb.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using Raven.Client;
    using Raven.Client.Linq;

    public class FakeRavenQueryable<T> : IRavenQueryable<T>
    {
        private readonly IQueryable<T> source;

        private RavenQueryStatistics QueryStatistics { get; set; }

        public FakeRavenQueryable(IQueryable<T> source, RavenQueryStatistics stats = null)
        {
            this.source = source;
            QueryStatistics = stats;
        }

        public IRavenQueryable<T> Customize(Action<IDocumentQueryCustomization> action)
        {
            return this;
        }

        public IRavenQueryable<TResult> TransformWith<TTransformer, TResult>() where TTransformer : AbstractTransformerCreationTask, new()
        {
            throw new NotImplementedException();
        }

	    public IRavenQueryable<TResult> TransformWith<TResult>(string transformerName)
	    {
		    throw new NotImplementedException();
	    }

	    public IRavenQueryable<T> AddQueryInput(string name, RavenJToken value)
        {
            throw new NotImplementedException();
        }

	    public IRavenQueryable<T> AddTransformerParameter(string name, RavenJToken value)
	    {
		    throw new NotImplementedException();
	    }

	    public IRavenQueryable<T> Spatial(Expression<Func<T, object>> path, Func<SpatialCriteriaFactory, SpatialCriteria> clause)
        {
            throw new NotImplementedException();
        }

	    public IRavenQueryable<T> OrderByDistance(SpatialSort sortParamsClause)
	    {
		    throw new NotImplementedException();
	    }

	    public IRavenQueryable<T> Statistics(out RavenQueryStatistics stats)
        {
            stats = QueryStatistics;
            return this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return source.GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return source.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return new FakeRavenQueryProvider(source, QueryStatistics); }
        }
    }
}
