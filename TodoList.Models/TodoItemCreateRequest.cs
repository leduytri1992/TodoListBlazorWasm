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
        public Guid Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public Priority Priority { get; set; }
    }
}
