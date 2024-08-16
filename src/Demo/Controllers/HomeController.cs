using Demo.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.Controllers;

public class HomeController(IConfiguration configuration, IOptions<ApiConfiguration> apiConfiguration)
    : Controller
{
    private readonly ApiConfiguration _apiConfig = apiConfiguration.Value;


    public async Task<IActionResult> Index()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var apiConfig = new ApiConfiguration();
        configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);
        
        var secret = apiConfig.UserSecret;
        var user = configuration[$"{ApiConfiguration.ConfigName}:UserKey"];
        
        var domain = _apiConfig.Domain;
        
        return View();
    }
}