using Newtonsoft.Json;
using System.Linq.Expressions;
using WebApplication6.Model.Service;

namespace WebApplication6.Model.Repository
{
    public class BaseRepository<T> : IRepository<T>
          where T : class, new()
    {
        private List<T> list = [];
        Server<T> server = new Server<T>();
        public Task<T> Add(T entity)
        {
            var items = server.ReadJsonFile().ToList();
            items.Add(entity);
            server.WriteToJsonFile(items);
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(Func<T, bool> predicate)
        {
            var items = server.ReadJsonFile().ToList();
            if (items.Remove(items.Where(predicate).FirstOrDefault()))
                server.WriteToJsonFile(items);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<T>> FindBy(Func<T, bool> predicate)
        {
            var items = server.ReadJsonFile().Where(predicate).AsEnumerable();
            return Task.FromResult(items);
        }

        public Task<List<T>> GetAll()
        {
            return Task.FromResult(server.ReadJsonFile().ToList());
        }


    }
}
