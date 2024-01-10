using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repositories;
using TodoList.Models;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUserList();
            var assigneeDtos = users.Select(x => new AssigneeDto
            {
                Id = x.Id,
                Name = x.UserName!
            });

            return Ok(assigneeDtos);
        }
    }
}
