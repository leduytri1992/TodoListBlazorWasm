using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoList.Models.SeedWork;
using TodoListBlazorWasm.Components;
using TodoListBlazorWasm.Pages.Components;
using TodoListBlazorWasm.Services;
using TodoListBlazorWasm.Shared;
using TodoListBlazorWasm.Utilities;

namespace TodoListBlazorWasm.Pages
{
    public partial class UserTodoItems
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

        [CascadingParameter]
        private Error Error { get; set; }

        protected override async Task OnInitializedAsync()
        {
			await GetMytasks();
		}

        public async Task SearchTodoItem(TaskListSearch searchRequest)
        {
            TaskListSearch = searchRequest;
			await GetMytasks();
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
                    await GetMytasks();
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
                await GetMytasks();
            }
        }

        protected async Task OnSelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetMytasks();
        }

        private async Task GetMytasks()
        {
            try
            {
				var pagingResponse = await TodoItemApi.GetMyTasks(TaskListSearch);
				TodoItems = pagingResponse.Items;
				MetaData = pagingResponse.MetaData;
			} 
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }
    }
}
