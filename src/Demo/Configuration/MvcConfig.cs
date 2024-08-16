using System.Reflection;
using Demo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Configuration;

public static class MvcConfig
{
    public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });
        
        // var provider = builder.Services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        // builder.Services.AddDbContext<AppDbContext>(o =>
        //     o.UseInMemoryDatabase("InMemoryDbForTesting").UseInternalServiceProvider(provider));

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));
        
        builder.Services.Configure<ApiConfiguration>(
            builder.Configuration.GetSection(ApiConfiguration.ConfigName));
        
        return builder;
    }
    
    public static WebApplication UseMvcConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }else
        {
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();
        }
        
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
        
        return app;
    }
}