using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoList.Api.Data;
using TodoList.Api.Entities;
using TodoList.Models;
using TodoList.Models.SeedWork;

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

        public async Task<PageList<TodoItem>> GetTodoList(TaskListSearch taskListSearch)
        {
            var query = _context.TodoItems
                .Include(x => x.Assignee).AsQueryable();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
            {
                query = query.Where(x => x.Name.Contains(taskListSearch.Name));
            }

            if (taskListSearch.AssigneeId.HasValue)
            {
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);
            }

            if (taskListSearch.Priority.HasValue)
            {
                query = query.Where(x => x.Priority == taskListSearch.Priority.Value);
            }

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreatedDate)
                .Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
                .Take(taskListSearch.PageSize)
                .ToListAsync();

            return new PageList<TodoItem>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
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

        public async Task<bool> Delete(Guid id)
        {
            TodoItem? findTodoItem = await GetById(id);
            if (findTodoItem != null)
            {
                _context.TodoItems.Remove(findTodoItem);
                await _context.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

		public async Task<PageList<TodoItem>> GetTodoListByUserId(Guid userId, TaskListSearch taskListSearch)
		{
			var query = _context.TodoItems
                .Where(x=>x.AssigneeId== userId)
				.Include(x => x.Assignee).AsQueryable();

			if (!string.IsNullOrEmpty(taskListSearch.Name))
			{
				query = query.Where(x => x.Name.Contains(taskListSearch.Name));
			}

			if (taskListSearch.AssigneeId.HasValue)
			{
				query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId.Value);
			}

			if (taskListSearch.Priority.HasValue)
			{
				query = query.Where(x => x.Priority == taskListSearch.Priority.Value);
			}

			var count = await query.CountAsync();

			var data = await query.OrderByDescending(x => x.CreatedDate)
				.Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize)
				.Take(taskListSearch.PageSize)
				.ToListAsync();

			return new PageList<TodoItem>(data, count, taskListSearch.PageNumber, taskListSearch.PageSize);
		}
	}
}
