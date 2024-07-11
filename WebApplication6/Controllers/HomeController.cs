using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Model.Entities;
using WebApplication6.Model.Repository;
using WebApplication6.Model.Service;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IRepository<User> userRepository = null;
        public HomeController(IRepository<User> repository)
        {
            userRepository = repository;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await Task.Run(() => userRepository.GetAll());
            return Ok(users);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var users = await Task.Run(() => userRepository.GetAll());
            int langht = users.Last().ID + 1;
            user.ID = langht;
            users.Add(user);


            return Ok(true);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {          
            var isDeleted = await Task.Run(async () => await userRepository.Delete(x => x.ID == id));
            if (!isDeleted) return NotFound();
            return NoContent();
        }
        [HttpPost("IsValidate")]
        public async Task<IActionResult> CheckIfUserIsValid(string username, string password)
        {
            var u = await Task.Run(() => userRepository.FindBy(x => x.UserName == username && x.UserPassword == password));
            if (u == null) return Unauthorized();

            return Ok(u);
        }

    }
}
