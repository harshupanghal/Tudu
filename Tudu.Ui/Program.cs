using Microsoft.EntityFrameworkCore;
using Tudu.Application.Interfaces;
using Tudu.Application.Services;
using Tudu.Infrastructure.Context;
using Tudu.Infrastructure.Repositories;
using Tudu.Ui.Components;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<TuduDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TuduConnection")));


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


const string AuthScheme = "tudu-auth";
const string AuthCookie = "tudu-cookie";

builder.Services.AddCascadingAuthenticationState();

// to setup auth , we will have to configure builder to use authentication services
builder.Services.AddAuthentication(AuthScheme)
    .AddCookie(AuthScheme, options =>
    {
        options.Cookie.Name = AuthCookie;
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth/access-denied";
        options.LogoutPath = "/auth/logout";

        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;

        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    }

if (!DbInitializer.InitializeDatabase(app.Services, out var error))
{
    Console.WriteLine(error); // Or use a logger
}
else
{
    Console.WriteLine("Database connection successful and tables ensured.");
}

app.UseHttpsRedirection();

app.UseStaticFiles();
// then we will use them here
app.UseAuthentication().
    UseAuthorization();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


public static class DbInitializer
{
    public static bool InitializeDatabase(IServiceProvider serviceProvider, out string errorMessage)
    {
        errorMessage = null;

        try
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TuduDbContext>();

            // This will apply any pending migrations and create the database if it doesn't exist
            context.Database.EnsureCreated(); // use EnsureCreated() if you're not using Migrations

            // Optional: test connection
            context.Database.CanConnect();

            return true;
        }
        catch (Exception ex)
        {
            errorMessage = $"Database initialization failed: {ex.Message}";
            return false;
        }
    }
}