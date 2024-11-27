using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proyecto_inmobiliaria_.Net.Models;
using Microsoft.AspNetCore.Diagnostics;  // Necesario para IExceptionHandlerFeature
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_inmobiliaria_.Net.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    
    { ViewBag.Id = TempData["Id"];
            if (TempData.ContainsKey("Mensaje")){
                ViewBag.Mensaje = TempData["Mensaje"];
            }
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Restringido(){
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
     public IActionResult Error(int? statusCode)
    {
        if (statusCode == 404)
        {
            ViewBag.ErrorMessage = "Lo sentimos, la página que buscas no existe.";
        }
        else
        {
            ViewBag.ErrorMessage = "Algo salió mal. Por favor, intenta de nuevo más tarde.";
        }

        return View();
    }
}