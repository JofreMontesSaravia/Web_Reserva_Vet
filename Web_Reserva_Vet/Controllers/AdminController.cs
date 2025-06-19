using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;
using Microsoft.EntityFrameworkCore;// Librerias que nos van ayudar a la creacion del crud
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization; //Librerias que nos van ayudar a la creacion del crud

namespace Web_Vet_Pet.Controllers
{
    //controller para la vista, aqui van agregando las vistas nuevas respecto a admin
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Titulo"] = "Dashboard";
            ViewData["Icono"] = "fa-solid fa-bars-staggered";
            return View();
        }

        public IActionResult Configuracion()
        {
            ViewData["Titulo"] = "Configuracion";
            ViewData["Icono"] = "fa-solid fa-gear";
            return View();
        }
        public IActionResult Reservas()
        {
            ViewData["Titulo"] = "Reservas";
            ViewData["Icono"] = "fa-regular fa-clipboard";
            return View();
        }
        public IActionResult Veterinarios()
        {
            ViewData["Titulo"] = "Veterinarios";
            ViewData["Icono"] = "fa-solid fa-user-doctor";
            return View();
        }
        public IActionResult Servicios()
        {
            ViewData["Titulo"] = "Servicios";
            ViewData["Icono"] = "fa-solid fa-shield-dog";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
