using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoListBlazorWasm;
using TodoListBlazorWasm.Services;
using TodoListBlazorWasm.Utilities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient<ITodoItemApiClient, TodoItemApiClient>();
builder.Services.AddTransient<IUserApiClient, UserApiClient>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp => new HttpClient
{ 
    BaseAddress = new Uri(builder.Configuration["BackendApiUrl"]!)
});

await builder.Build().RunAsync();
