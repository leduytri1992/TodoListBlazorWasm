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

        public async Task<List<TodoItemDto>> GetTodoItemsList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TodoItemDto>>("/api/TodoItems");
            return result;
        }
    }
}
