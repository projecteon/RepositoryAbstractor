namespace Repository
{
    public interface IUnitOfWork
    {
        void Commit();

        void Dispose();

        void Rollback();
    }
}