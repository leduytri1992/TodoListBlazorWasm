using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Entities;

namespace TodoList.Api.Data
{
    public class TodoListDbContext : IdentityDbContext<User, Role, Guid>
    {

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
