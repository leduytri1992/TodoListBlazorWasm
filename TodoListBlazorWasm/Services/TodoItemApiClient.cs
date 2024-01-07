using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using TodoList.Models;
using TodoList.Models.SeedWork;

namespace TodoListBlazorWasm.Services
{
    public class TodoItemApiClient: ITodoItemApiClient
    {
        public HttpClient _httpClient;

        public TodoItemApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateTodoItem(TodoItemCreateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/todoItems", request);
            return response.IsSuccessStatusCode;
        }

        public async Task<TodoItemDto> GetTodoItem(string id)
		{
			var response = await _httpClient.GetFromJsonAsync<TodoItemDto>($"/api/todoItems/{id}");
			return response!;
		}

		public async Task<PageList<TodoItemDto>> GetTodoList(TaskListSearch taskListSearch)
        {
            var queryString = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
			};

            if (!string.IsNullOrEmpty(taskListSearch.Name))
            {
                queryString.Add("name", taskListSearch.Name);
            }
			if (taskListSearch.AssigneeId.HasValue)
			{
				queryString.Add("assigneeId", taskListSearch.AssigneeId.ToString());
			}
			if (taskListSearch.Priority.HasValue)
			{
				queryString.Add("priority", taskListSearch.Priority.ToString());
			}

            string url = QueryHelpers.AddQueryString("api/todoItems", queryString);
			var response = await _httpClient.GetFromJsonAsync<PageList<TodoItemDto>>(url);
            return response!;
        }

		public async Task<bool> UpdateTodoItem(string id, TodoItemUpdateRequest request)
		{
            var response = await _httpClient.PutAsJsonAsync($"api/todoItems/{id}", request);
            return response.IsSuccessStatusCode;
		}

        public async Task<bool> DeleteTodoItem(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/todoItems/{id}");
            return response.IsSuccessStatusCode;
        }

		public async Task<bool> AssignTask(Guid id, AssignTaskRequest request)
		{
            var response = await _httpClient.PutAsJsonAsync($"api/todoItems/{id}/assign", request);
            return response.IsSuccessStatusCode;
		}
	}
}
