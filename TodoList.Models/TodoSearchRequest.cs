using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TodoSearchRequest
    {
        public string? Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public Priority? Priority { get; set; }
    }
}
