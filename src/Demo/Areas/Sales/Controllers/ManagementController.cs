using Microsoft.AspNetCore.Mvc;
namespace Demo.Areas.Sales.Controllers;

[Area("Sales")]
[Route("gestao-vendas")]
public class ManagementController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}