using Demo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var provider = builder.Services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseInMemoryDatabase("InMemoryDbForTesting").UseInternalServiceProvider(provider));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}");

app.Run();