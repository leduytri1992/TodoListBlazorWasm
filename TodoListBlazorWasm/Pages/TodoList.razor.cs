using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoList.Models.Enums;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITodoItemApiClient TaskApiClient { get; set; }
        [Inject] private IUserApiClient UserApiClient { get; set; }

        private List<TodoItemDto> TodoItems;

        private TaskListSearch TaskListSearch = new TaskListSearch { };

        private List<AssigneeDto> Assignees;

        protected override async Task OnInitializedAsync()
        {
            TodoItems = await TaskApiClient.GetTodoItemsList();
            Assignees = await UserApiClient.GetUserList();
        }
    }

    public class TaskListSearch
    {
        public string Name { get; set; }

        public Guid AssigneeId { get; set; }

        public Priority Priority { get; set; }
    }
}
