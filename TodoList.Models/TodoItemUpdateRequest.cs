using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TodoList.Models.Enums;

namespace TodoList.Models
{
    public class TodoItemUpdateRequest
    {
        public string Name { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
