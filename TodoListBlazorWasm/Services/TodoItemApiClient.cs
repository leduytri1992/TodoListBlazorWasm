using System.Net.Http.Json;
using TodoList.Models;

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

		public async Task<List<TodoItemDto>> GetTodoList(TodoSearchRequest todoListSearch)
        {
            string url = $"/api/todoItems?name={todoListSearch.Name}&assigneeId={todoListSearch.AssigneeId}&priority={todoListSearch.Priority}";
            var response = await _httpClient.GetFromJsonAsync<List<TodoItemDto>>(url);
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
