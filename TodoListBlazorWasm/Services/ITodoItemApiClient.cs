using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface ITodoItemApiClient
    {
        Task<List<TodoItemDto>> GetTodoList(TodoSearchRequest todoListSearch);

        Task<TodoItemDto> GetTodoItem(string id);

        Task<bool> CreateTodoItem(TodoItemCreateRequest request);

		Task<bool> UpdateTodoItem(string id, TodoItemUpdateRequest request);

        Task<bool> DeleteTodoItem(Guid id);

        Task<bool> AssignTask(Guid id, AssignTaskRequest request);
    }
}
