using TodoList.Api.Entities;
using TodoList.Models;

namespace TodoList.Api.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
