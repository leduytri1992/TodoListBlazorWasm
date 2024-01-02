using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        private TodoListSearch TodoListSearch = new TodoListSearch();

        private List<AssigneeDto> Assignees;

        protected override async Task OnInitializedAsync()
        {
            TodoItems = await TaskApiClient.GetTodoItemList(TodoListSearch);
            Assignees = await UserApiClient.GetUserList();
        }

        protected async Task SearchForm(EditContext context)
        {
            // await Console.Out.WriteLineAsync(TodoListSearch.Name);
            TodoItems = await TaskApiClient.GetTodoItemList(TodoListSearch);
        }
    }
}
