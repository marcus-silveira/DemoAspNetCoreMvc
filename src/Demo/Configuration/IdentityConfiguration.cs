using Demo.Data;
using Microsoft.AspNetCore.Identity;

namespace Demo.Configuration;

public static class IdentityConfiguration
{
    public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("CanPermanentlyDelete", policy =>
                policy.RequireRole("Admin"))
    
            .AddPolicy("ViewProduct", policy =>
                policy.RequireClaim("Product","View"));

        return builder;
    }
}