using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITodoItemApiClient TodoItemApi { get; set; }
        [Inject] protected PreloadService PreloadService { get; set; }
        [Inject] protected ToastService? ToastService { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }

        private List<TodoItemDto>? TodoItems;

        private TodoSearchRequest SearchRequest = new TodoSearchRequest();

        protected override async Task OnInitializedAsync()
        {
            TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
        }

        public async Task SearchTodoItem(TodoSearchRequest searchRequest)
        {
            SearchRequest = searchRequest;
            TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
        }

        protected async Task DeleteTodoItem(Guid id)
        {
            var isSuccess = await TodoItemApi.DeleteTodoItem(id); 
            if (isSuccess)
	        {
		        ToastService?.Notify(CreateToastMessage(ToastType.Success, "Successfully"));
                TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
            }
            else
	        {
		        ToastService?.Notify(CreateToastMessage(ToastType.Warning, "Some thing went wrong :)"));
	        }
	}

	private ToastMessage CreateToastMessage(ToastType toastType, string message)
        => new ToastMessage
        {
            Type = toastType,
            Title = "Delete task",
            HelpText = $"{DateTime.Now}",
            Message = message,
        };
    }
}
