using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;
using Microsoft.EntityFrameworkCore;// Librerias que nos van ayudar a la creacion del crud
using Microsoft.AspNetCore.Mvc.Rendering; //Librerias que nos van ayudar a la creacion del crud

namespace Web_Reserva_Vet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /*

        public IActionResult Privacy()
        {
            return View();
        }
        */



        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
    }
}
