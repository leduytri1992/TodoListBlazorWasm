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
            var result = await _httpClient.PostAsJsonAsync("/api/todoItems", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<TodoItemDto> GetTodoItemDetail(string id)
		{
			var result = await _httpClient.GetFromJsonAsync<TodoItemDto>($"/api/todoItems/{id}");
			return result!;
		}

		public async Task<List<TodoItemDto>> GetTodoList(TodoSearchRequest todoListSearch)
        {
            string url = $"/api/todoItems?name={todoListSearch.Name}&assigneeId={todoListSearch.AssigneeId}&priority={todoListSearch.Priority}";
            var result = await _httpClient.GetFromJsonAsync<List<TodoItemDto>>(url);
            return result!;
        }
    }
}
