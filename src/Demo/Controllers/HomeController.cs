using Demo.Configuration;
using Demo.Models;
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
    
    [Route("error-test")]
    public IActionResult ErrorTest()
    {
        throw new Exception("ALGO ERRADO NÃO ESTAVA CERTO!");

        return View("Index");
    }

    [Route("error/{code:length(3,3):int}")]
    public async Task<IActionResult> Errors(int code)
    {
        var modelError = new ErrorViewModel();

        switch (code)
        {
            case 500:
                modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelError.Title = "Ocorreu um erro!";
                modelError.ErrorCode = code;
                break;
            case 404:
                modelError.Message = "A página que está procurando não existe! <br />Em caso de dúvcodeas entre em contato com nosso suporte";
                modelError.Title = "Ops! Página não encontrada.";
                modelError.ErrorCode = code;
                break;
            case 403:
                modelError.Message = "Você não tem permissão para fazer isto.";
                modelError.Title = "Acesso Negado";
                modelError.ErrorCode = code;
                break;
            default:
                return StatusCode(500);
        }

        return View("Error", modelError);
    }
}