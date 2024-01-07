using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoListBlazorWasm.Components;
using TodoListBlazorWasm.Pages.Components;
using TodoListBlazorWasm.Services;
using TodoListBlazorWasm.Utilities;

namespace TodoListBlazorWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITodoItemApiClient TodoItemApi { get; set; }
        [Inject] protected PreloadService PreloadService { get; set; }
        [Inject] protected ToastService? ToastService { get; set; }

        private List<TodoItemDto>? TodoItems;

        private TodoSearchRequest SearchRequest = new TodoSearchRequest();

        private Guid Id { get; set; }
        protected Confirmation DeleteConfirmation { get; set; }

        protected AssignTask AssignTaskDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
        }

        public async Task SearchTodoItem(TodoSearchRequest searchRequest)
        {
            SearchRequest = searchRequest;
            TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
        }

        protected async Task OnDeleteItem(Guid id)
        {
            Id = id;
            await DeleteConfirmation.ShowAsync();
        }

        protected async Task OnDeleteConfirm(bool isConfirmed)
        {
            if (isConfirmed)
            {
                var isSuccess = await TodoItemApi.DeleteTodoItem(Id);
                if (isSuccess)
                {
                    ToastService?.Notify(ViewUtils.CreateToastMessage(ToastType.Primary, "Delete task successful"));
                    TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
                }
                else
                {
                    ToastService?.Notify(ViewUtils.CreateToastMessage(ToastType.Warning, "Some thing went wrong :)"));
                }
            }
        }

        protected void OnAssignTask(Guid id)
        {
            AssignTaskDialog?.Show(id);
        }

        protected async Task OnAssignTaskDone(bool result)
        {
            if (result)
            {
                TodoItems = await TodoItemApi.GetTodoList(SearchRequest);
            }
        }
    }
}
