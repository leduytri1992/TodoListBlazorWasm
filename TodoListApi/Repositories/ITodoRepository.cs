using TodoList.Api.Entities;

namespace TodoList.Api.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAll();

        Task<TodoItem> Create(TodoItem todoItem);

        Task<TodoItem> Update(TodoItem todoItem);

        Task<TodoItem> Delete(Guid id);

        Task<TodoItem> GetById(Guid id);
    }
}
