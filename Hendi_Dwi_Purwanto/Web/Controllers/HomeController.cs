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
    private readonly IGenericRepository<Product> _genericRepository;
    public HomeController(ILogger<HomeController> logger,IGenericRepository<Product> genericRepository)
    {
        _logger = logger;
        _genericRepository = genericRepository;
    }

    public async Task<IActionResult> Index()
    {
        var categoriesWithProducts = await _genericRepository.GetAllAsync(c => c.Category);

        var model = await _genericRepository.GetAllAsync();
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
