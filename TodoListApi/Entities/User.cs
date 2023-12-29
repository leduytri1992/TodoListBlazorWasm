using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Entities
{
    public class User : IdentityUser<Guid>
    {
    }
}
