using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface ITodoItemApiClient
    {
        Task<List<TodoItemDto>> GetTodoItemList(TodoListSearch todoListSearch);

        Task<TodoItemDto> GetTodoItemDetail(string id);
    }
}
