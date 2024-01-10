using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface IUserApiClient
    {
        Task<List<AssigneeDto>?> GetUserList();
    }
}
