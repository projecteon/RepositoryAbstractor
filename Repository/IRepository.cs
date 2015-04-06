namespace Repository
{
    public interface IRepository<T> : IPersistableRepository<T>, IReadOnlyRepository<T>, IStringKeyedRepository<T> where T : class, IPersistable
    {
    }
}