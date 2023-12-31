using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Entities;

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

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
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
