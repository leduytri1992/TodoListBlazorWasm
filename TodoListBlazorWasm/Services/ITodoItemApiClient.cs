using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface ITodoItemApiClient
    {
        Task<List<TodoItemDto>> GetTodoItemsList();

        Task<TodoItemDto> GetTodoItemById(string id);
    }
}
