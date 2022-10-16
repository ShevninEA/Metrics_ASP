using MetricsManager.Models;

namespace MetricsManager.Services
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Delete(int item);
    }
}
