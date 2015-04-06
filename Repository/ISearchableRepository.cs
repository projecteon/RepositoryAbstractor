namespace Repository
{
    using System.Collections.Generic;

    public interface ISearchableRepository<T> where T : IPersistable
    {
        IList<T> SearchByAny(IEnumerable<string> searchParams); 
        IList<T> SearchByAll(IEnumerable<string> searchParams); 
    }
}