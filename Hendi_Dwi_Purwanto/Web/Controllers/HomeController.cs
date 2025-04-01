using System.Diagnostics;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGenericService<Product> _genericService;
    public HomeController(ILogger<HomeController> logger,IGenericService<Product> genericService)
    {
        _logger = logger;
        _genericService = genericService;
    }

    public async Task<IActionResult> Index()
    {
        var categoriesWithProducts = await _genericService.GetAllAsync(c => c.Category);

        var model = await _genericService.GetAllAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
