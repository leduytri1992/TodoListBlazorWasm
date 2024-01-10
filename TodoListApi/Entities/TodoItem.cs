using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.Models.Enums;

namespace TodoList.Api.Entities
{
    public class TodoItem
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        [ForeignKey(nameof(AssigneeId))]
        public User Assignee { get; set; }

        public DateTime CreatedDate { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }  
    }
}
