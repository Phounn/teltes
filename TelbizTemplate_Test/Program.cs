using MudBlazor.Services;
using TelbizTemplate_Test.Components;
using TelbizTemplate_Test.Components.Pages.FetchDataByRefit;
using System.Net.Http.Json;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient();
builder.Services.AddHttpClient("meta", c =>
{
    c.BaseAddress = new Uri("https://jsonplaceholder.org/");
});

builder.Services.AddMudServices();
builder.Services.AddRefitClient<IFetchEmp>().ConfigureHttpClient(x =>
{
    x.BaseAddress = new Uri("https://jsonplaceholder.org/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
