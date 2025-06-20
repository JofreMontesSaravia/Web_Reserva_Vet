using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Web_Vet_Pet.Controllers
{
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
