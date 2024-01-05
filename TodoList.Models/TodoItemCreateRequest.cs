using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TodoItemCreateRequest
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(30, ErrorMessage = "Max length is 30 characters")]
        [Required(ErrorMessage = "Please enter task name")]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Please select task priority")]
        public Priority? Priority { get; set; }
    }
}
