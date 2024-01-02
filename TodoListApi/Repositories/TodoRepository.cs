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

        public async Task<TodoItem> GetById(Guid id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetTodoList(TodoListSearch todoListSearch)
        {
            var query = _context.TodoItems
                .Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(todoListSearch.Name))
            {
                query = query.Where(x => x.Name.Contains(todoListSearch.Name));
            }

            if (todoListSearch.AssigneeId.HasValue)
            {
                query = query.Where(x => x.AssigneeId == todoListSearch.AssigneeId.Value);
            }

            if (todoListSearch.Priority.HasValue)
            {
                query = query.Where(x => x.Priority == todoListSearch.Priority.Value);
            }

            return await query.ToListAsync();
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
        public async Task<TodoItem> Delete(Guid id)
        {
            TodoItem todoItem = await GetById(id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
    }
}
