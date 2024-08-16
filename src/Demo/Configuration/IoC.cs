namespace Demo.Configuration;

public static class IoC
{
    public static WebApplicationBuilder AddIoCConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}