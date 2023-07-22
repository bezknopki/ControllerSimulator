namespace ControllerSimulator.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? Get(int id);
    }
}
