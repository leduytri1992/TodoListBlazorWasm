using TodoList.Models;
using TodoList.Models.SeedWork;

namespace TodoListBlazorWasm.Services
{
    public interface ITodoItemApiClient
    {
        Task<PageList<TodoItemDto>> GetTodoList(TaskListSearch todoListSearch);

        Task<PageList<TodoItemDto>> GetMyTasks(TaskListSearch taskListSearch);

        Task<TodoItemDto> GetTodoItem(string id);

        Task<bool> CreateTodoItem(TodoItemCreateRequest request);

		Task<bool> UpdateTodoItem(string id, TodoItemUpdateRequest request);

        Task<bool> DeleteTodoItem(Guid id);

        Task<bool> AssignTask(Guid id, AssignTaskRequest request);
    }
}
