using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using Tudu.Application.Interfaces;
using Tudu.Application.Services;
using Tudu.Infrastructure.Context;
using Tudu.Infrastructure.Repositories;
using Tudu.Ui.Components;

var builder = WebApplication.CreateBuilder(args);

// UI + Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

// EF DB
builder.Services.AddDbContext<TuduDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TuduConnection")));

// Application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();

// Repositories & security
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddAuthentication().AddCookie();

// Authentication setup (cookie)
const string AuthScheme = "tudu-auth";
const string AuthCookie = "tudu-cookie";

// Add Authentication & Authorization
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

builder.Services.AddAuthorization();

// Add the built-in AuthenticationStateProvider that integrates with server cookie auth.
// The server already knows how to provide authentication state from the HttpContext user.
builder.Services.AddScoped<AuthenticationStateProvider,
    ServerAuthenticationStateProvider>(); // ServerAuthenticationStateProvider lives in Blazor Server assemblies

var app = builder.Build();

// Middleware order
if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
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

app.UseAuthentication();
app.UseAuthorization();

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