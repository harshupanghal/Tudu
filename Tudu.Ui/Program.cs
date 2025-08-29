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
using Tudu.Application.Validators;
using Tudu.Application.Validations;
using FluentValidation;
using Blazored.LocalStorage;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContext<TuduDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TuduConnection")));
// "TuduConnection": "Server=(localdb)\\mssqllocaldb;Database=TuduDb;Trusted_Connection=True;"


builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddAuthentication().AddCookie();

const string AuthScheme = "tudu-auth";
const string AuthCookie = "tudu-cookie";

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

builder.Services.AddScoped<AuthenticationStateProvider,
    ServerAuthenticationStateProvider>();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserTaskRequestValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    {
    var db = scope.ServiceProvider.GetRequiredService<TuduDbContext>();
    db.Database.Migrate();
    }

if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
