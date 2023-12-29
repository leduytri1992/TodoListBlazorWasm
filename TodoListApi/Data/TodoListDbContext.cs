using Microsoft.EntityFrameworkCore;

namespace TodoList.Api.Data
{
    public class TodoListDbContext: DbContext
    {

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {
            
        }
    }
}
