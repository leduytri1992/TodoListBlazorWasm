using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface ITodoItemApiClient
    {
        Task<List<TodoItemDto>> GetTodoList(TodoSearchRequest todoListSearch);

        Task<TodoItemDto> GetTodoItemDetail(string id);
    }
}
