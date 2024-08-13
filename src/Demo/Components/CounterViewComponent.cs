using Microsoft.AspNetCore.Mvc;

namespace Demo.Components;

public class CounterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int modelCounter)
    {
        return View(modelCounter);
    }
}