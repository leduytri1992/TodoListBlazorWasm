using TodoList.Api.Entities;
using TodoList.Models;
using TodoList.Models.SeedWork;

namespace TodoList.Api.Repositories
{
    public interface ITodoRepository
    {
        Task<PageList<TodoItem>> GetTodoList(TaskListSearch request);

        Task<PageList<TodoItem>> GetTodoListByUserId(Guid userId, TaskListSearch request);

        Task<TodoItem> Create(TodoItem todoItem);

        Task<TodoItem> Update(TodoItem todoItem);

        Task<bool> Delete(Guid id);

        Task<TodoItem?> GetById(Guid id);
    }
}
