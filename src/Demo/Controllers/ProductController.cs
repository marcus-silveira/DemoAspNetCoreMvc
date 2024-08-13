using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers;

[Route("meus-produtos")]
public class ProductController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await context.Products.ToListAsync());
    }

    [Route("detalhes/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var product = await context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }
    
    [Route("criar-novo")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("criar-novo")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Image,Price")] Product product)
    {
        if (!ModelState.IsValid) return View(product);
        context.Add(product);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Route("editar/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var product = await context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost("editar/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image,Price")] Product product)
    {
        if (id != product.Id) return NotFound();

        if (!ModelState.IsValid) return View(product);
        try
        {
            context.Update(product);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(product.Id)) return NotFound();

            throw;
        }

        return RedirectToAction(nameof(Index));
    }
    
    [Route("deletar/{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var product = await context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost("deletar/{id}")]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null) context.Products.Remove(product);

        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return context.Products.Any(e => e.Id == id);
    }
}