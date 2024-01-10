using System.ComponentModel.DataAnnotations;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TodoItemUpdateRequest
	{
		[MaxLength(30, ErrorMessage = "Max length is 30 characters")]
		[Required(ErrorMessage = "Please enter task name")]
		public string Name { get; set; }

		public Guid? AssigneeId { get; set; }

		public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
