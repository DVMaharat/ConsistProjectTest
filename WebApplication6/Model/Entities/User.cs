using System.ComponentModel.DataAnnotations;
using WebApplication6.Model.Repository;

namespace WebApplication6.Model.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

    }

}
