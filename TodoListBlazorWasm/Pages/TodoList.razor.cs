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

        private TodoSearchRequest SearchRequest = new TodoSearchRequest();

        private List<AssigneeDto> Assignees;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            TodoItems = await TaskApiClient.GetTodoList(SearchRequest);
            Assignees = await UserApiClient.GetUserList();
        }

        protected async Task SearchForm(EditContext context)
        {
            // Console.WriteLine(TodoListSearch.Name);
            TodoItems = await TaskApiClient.GetTodoList(SearchRequest);
        }
    }
}
