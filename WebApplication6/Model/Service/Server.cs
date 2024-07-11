using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
using WebApplication6.Model.Entities;
using System.IO.Pipes;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication6.Model.Service
{
    public class Server<T> where T : class, new()
    {
        public Server()
        {
            Init();
        }
        public void Init()
        {
            var items = ReadJsonFile();
            if (!items.Any())
            {
                var _Users = new List<User>
        {
            new User { ID = 1, UserName = "Moshe", UserPassword = "password1" },
            new User { ID = 2, UserName = "David", UserPassword = "password2" },
            new User { ID = 3, UserName = "Or", UserPassword = "password3" }
        };
                WriteToJsonFile(_Users);
            }
        }

        public IEnumerable<T> ReadJsonFile()
        {
            string itemPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\{typeof(T).Name}.json";

            if (File.Exists(itemPath))
            {
                var text = File.ReadAllText(itemPath);
                var items = JsonConvert.DeserializeObject<IEnumerable<T>>(text, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });


                return items;
            }
            return default;
        }

        public void WriteToJsonFile<T>(IEnumerable<T> values)
        {
            string itemPath = $@"{AppDomain.CurrentDomain.BaseDirectory}\{typeof(T).Name}.json";
            if (!File.Exists(itemPath))
            {
                File.Create(itemPath).Dispose();
            }

            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            File.WriteAllText(itemPath, json);
        }
    }
}
