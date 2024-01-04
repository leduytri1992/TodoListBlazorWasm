using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Entities;
using TodoList.Models;

namespace TodoList.Api.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoListDbContext _context;

        public TodoRepository(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> GetById(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoList(TodoSearchRequest request)
        {
            var query = _context.TodoItems
                .Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (request.AssigneeId.HasValue)
            {
                query = query.Where(x => x.AssigneeId == request.AssigneeId.Value);
            }

            if (request.Priority.HasValue)
            {
                query = query.Where(x => x.Priority == request.Priority.Value);
            }

            return await query.OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<TodoItem> Create(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> Update(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem?> Delete(Guid id)
        {
            TodoItem? findTodoItem = await GetById(id);
            if (findTodoItem != null)
            {
                _context.TodoItems.Remove(findTodoItem);
                await _context.SaveChangesAsync();
            }
            
            return findTodoItem;
        }
    }
}
