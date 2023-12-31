using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITodoItemApiClient HttpClient { get; set; }

        private List<TodoItemDto> todoItems;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(1000);
            todoItems = await HttpClient.GetTodoItemsList();
        }
    }
}
