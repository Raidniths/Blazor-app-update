using Blazor_app.Components;
using Blazor_app.Interfaces;
using Blazor_app.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Registrera HttpClient och UserService
builder.Services.AddHttpClient();
builder.Services.AddScoped<MockUserService>();
builder.Services.AddScoped<ApiUserService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();