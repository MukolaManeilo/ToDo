using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoContext") ?? throw new InvalidOperationException("Connection string 'ToDoContext' not found.")));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}
app.UseCsp(options =>
{
    options.DefaultSources(s => s.Self());
    options.StyleSources(s => s.Self().UnsafeInline());
    options.ScriptSources(s => s.Self().UnsafeInline());
});

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();