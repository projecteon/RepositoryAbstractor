namespace Repository
{
    public interface IPersistableRepository<T> where T : IPersistable
    {
        void Add(T entity);

        void Delete(T entity);

        bool Update(T entity);
    }
}