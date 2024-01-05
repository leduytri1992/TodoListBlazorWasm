using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Entities;
using TodoList.Api.Repositories;
using TodoList.Models;
using TodoList.Models.Enums;

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

        // api/todoItems?name=&priority=&status=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TodoSearchRequest request)
        {
            var todoItems = await _todoRepository.GetTodoList(request);
            var todoItemsDto = todoItems
                .Select(x => new TodoItemDto()
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                AssigneeId = x.AssigneeId,
                Priority = x.Priority,
                CreatedDate = x.CreatedDate,
                AssigneeName = x.Assignee != null ? x.Assignee.UserName! : "N/A"
            }).ToList();

            return Ok(todoItemsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItemCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todoItem = await _todoRepository.Create(new TodoItem
            {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                Id = request.Id,
                CreatedDate = DateTime.Now

            });

            return CreatedAtAction(nameof(GetById), new { id = request.Id }, todoItem);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, TodoItemUpdateRequest request)
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

            todoItemDb.Name = request.Name;
            todoItemDb.Priority = request.Priority;
            todoItemDb.Status = request.Status;
            todoItemDb.AssigneeId = request.AssigneeId ?? null;

            var itemResult = await _todoRepository.Update(todoItemDb);

            return Ok(new TodoItemDto()
            {
                Id = itemResult.Id,
                Name = itemResult.Name,
                Status = itemResult.Status,
                AssigneeId = itemResult.AssigneeId,
                Priority = itemResult.Priority,
                CreatedDate = itemResult.CreatedDate
            });
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
            return Ok(new TodoItemDto()
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                Status = todoItem.Status,
                AssigneeId = todoItem.AssigneeId,
                Priority = todoItem.Priority,
                CreatedDate = todoItem.CreatedDate
            });
        }
    }
}
