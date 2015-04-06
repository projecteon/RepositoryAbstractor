namespace Repository
{
    public interface IStringKeyedRepository<T> where T : class
    {
        T SingleBy(string id);
    }
}
