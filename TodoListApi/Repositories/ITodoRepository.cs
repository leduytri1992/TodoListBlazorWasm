using TodoList.Api.Entities;
using TodoList.Models;

namespace TodoList.Api.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoList(TodoSearchRequest request);

        Task<TodoItem> Create(TodoItem todoItem);

        Task<TodoItem> Update(TodoItem todoItem);

        Task<bool> Delete(Guid id);

        Task<TodoItem?> GetById(Guid id);
    }
}
