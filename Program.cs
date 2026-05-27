using CodieGo_Adventure.Data;
using CodieGo_Adventure.Filters;
using CodieGo_Adventure.Repository;
using CodieGo_Adventure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injection for Generic Repository and Services
builder.Services.AddScoped<IGenericRepository, GenericRepository>();

builder.Services.AddScoped<IGenericServices, GenericServices>();

builder.Services.AddScoped<RequireSessionFilter>();
builder.Services.AddScoped<RedirectIfAuthenticatedFilter>();
builder.Services.AddScoped<ContinueToSessionFilter>();

builder.Services.AddSingleton(new EmailService(
    host: "smtp.gmail.com",      // server
    port: 587,                   // port for TLS
    user: "codiegoadventure@gmail.com",
    pass: "hibsiohoxprreksf"
));

builder.Services.AddDbContext<CGADbContext>(options =>
    options.UseMySql(
        Environment.GetEnvironmentVariable("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 42))
    ));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Validation/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Landing}/{action=Page}/{id?}");

app.Run();
