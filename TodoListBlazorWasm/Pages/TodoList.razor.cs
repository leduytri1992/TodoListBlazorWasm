using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoList.Models.SeedWork;
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
        private MetaData MetaData { get; set; } = new MetaData();

        private TaskListSearch TaskListSearch = new TaskListSearch();

        private Guid Id { get; set; }
        protected Confirmation DeleteConfirmation { get; set; }

        protected AssignTask AssignTaskDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
			await GetTodoItems();
		}

        public async Task SearchTodoItem(TaskListSearch searchRequest)
        {
            TaskListSearch = searchRequest;
			await GetTodoItems();
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
                    await GetTodoItems();
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
                await GetTodoItems();
            }
        }

        protected async Task OnSelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetTodoItems();
        }

        private async Task GetTodoItems()
        {
            var pagingResponse = await TodoItemApi.GetTodoList(TaskListSearch);
            TodoItems = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
    }
}
