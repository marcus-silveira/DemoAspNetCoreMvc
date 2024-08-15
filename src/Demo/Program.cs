using Demo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

builder.Services.AddControllersWithViews();

// var provider = builder.Services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
// builder.Services.AddDbContext<AppDbContext>(o =>
//     o.UseInMemoryDatabase("InMemoryDbForTesting").UseInternalServiceProvider(provider));q

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CanPermanentlyDelete", policy =>
        policy.RequireRole("Admin"))
    
    .AddPolicy("ViewProduct", policy =>
        policy.RequireClaim("Product","View"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name:"default",
    pattern:"{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();