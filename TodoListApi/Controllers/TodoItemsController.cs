using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Entities;
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

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todoItems = await _todoRepository.Create(todoItem);
            return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItems);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoItemDb = await _todoRepository.GetById(id);
            if (todoItemDb == null)
            {
                return NotFound($"Find by {id} is not found");
            }

            var todoItems = await _todoRepository.Update(todoItem);
            return Ok(todoItems);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todoItem = await _todoRepository.GetById(id);
            if (todoItem == null)
            {
                return NotFound($"Find by {id} is not found");
            }
            return Ok(todoItem);
        }
    }
}
