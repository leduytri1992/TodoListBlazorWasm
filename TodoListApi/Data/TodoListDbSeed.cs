using Microsoft.AspNetCore.Identity;
using TodoList.Api.Entities;
using TodoList.Models.Enums;

namespace TodoList.Api.Data
{
    public class TodoListDbSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbSeed> logger)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = "s1mple.dev@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "1234567890",
                    UserName = "Admin1",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123");  
                context.Users.Add(user);
            }

            if (!context.TodoItems.Any())
            {
                context.TodoItems.Add(new TodoItem
                {
                    Id = Guid.NewGuid(),
                    Name = "Learn .NET",
                    AssigneeId = null,
                    CreatedDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
