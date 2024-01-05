using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoList.Models;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITodoItemApiClient TaskApiClient { get; set; }
        [Inject] private IUserApiClient UserApiClient { get; set; }
        [Inject] protected PreloadService PreloadService { get; set; }
        [Inject] protected ToastService? ToastService { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }


        private List<TodoItemDto>? TodoItems;

        private TodoSearchRequest SearchRequest = new TodoSearchRequest();

        private List<AssigneeDto>? Assignees;

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            TodoItems = await TaskApiClient.GetTodoList(SearchRequest);
            Assignees = await UserApiClient.GetUserList();
        }

        protected async Task SearchForm(EditContext context)
        {
            //Console.WriteLine(SearchRequest.Name);
            TodoItems = await TaskApiClient.GetTodoList(SearchRequest);
        }

        protected async Task DeleteTodoItem(string id)
        {
            var isSuccess = await TaskApiClient.DeleteTodoItem(id); 
            if (isSuccess)
	        {
		        ToastService?.Notify(CreateToastMessage(ToastType.Success, "Successfully"));
		        //NavigationManager.NavigateTo("/todoList");
                StateHasChanged();
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
