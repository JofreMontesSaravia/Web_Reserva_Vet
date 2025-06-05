using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;
using Microsoft.EntityFrameworkCore;// Librerias que nos van ayudar a la creacion del crud
using Microsoft.AspNetCore.Mvc.Rendering; //Librerias que nos van ayudar a la creacion del crud

namespace Web_Vet_Pet.Controllers
{
    //controller para la vista, aqui van agregando las vistas nuevas respecto a admin
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Name_Vet = "Clínica Veterinaria Patitas";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
