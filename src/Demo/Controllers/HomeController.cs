using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}