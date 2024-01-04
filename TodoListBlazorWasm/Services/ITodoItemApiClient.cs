using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface ITodoItemApiClient
    {
        Task<List<TodoItemDto>> GetTodoList(TodoSearchRequest todoListSearch);

        Task<TodoItemDto> GetTodoItem(string id);

        Task<bool> CreateTodoItem(TodoItemCreateRequest request);
    }
}
