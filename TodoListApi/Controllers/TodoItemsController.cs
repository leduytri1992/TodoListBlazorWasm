using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repositories;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoItemsController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todoItems = await _todoRepository.GetAll();
            return Ok(todoItems);
        }
    }
}
