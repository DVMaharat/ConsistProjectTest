using System.Linq.Expressions;

namespace WebApplication6.Model.Repository
{
    public interface IRepository<T> where T : class, new()
    {
       Task<List<T>> GetAll();

        Task<IEnumerable<T>> FindBy(Func<T, bool> predicate);

        Task<T> Add(T entity);

        Task<bool> Delete(Func<T, bool> predicate);
    }
}
