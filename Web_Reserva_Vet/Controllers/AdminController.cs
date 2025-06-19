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

        public IActionResult Servicios()
        {
            ViewBag.Name_Vet = "Clínica Veterinaria Patitas";
            var servicios = _context.Services
                .Select(s => new ServicioAdminViewModel
                {
                    Id = s.Id,
                    Nombre = s.Name,
                    Duracion = s.Duration,
                    Costo = s.Cost,
                    Veterinario = s.Appointments
                        .OrderBy(a => a.Id)
                        .Select(a => a.Veterinarian != null ? a.Veterinarian.Name : "-")
                        .FirstOrDefault() ?? "-",
                    Especie = s.Appointments
                        .OrderBy(a => a.Id)
                        .Select(a => a.Pet != null && a.Pet.TypePet != null ? a.Pet.TypePet.Species : "-")
                        .FirstOrDefault() ?? "-"
                })
                .ToList();
            return View(servicios);
        }

        public IActionResult Veterinarios()
        {
            ViewBag.Name_Vet = "Clínica Veterinaria Patitas";
            var veterinarios = _context.Veterinarians
                .Select(v => new VeterinarioAdminViewModel
                {
                    Id = v.Id,
                    Nombre = v.Name,
                    Email = v.Email,
                    Jornada = v.Shift + " horas",
                    PhoneNumber = v.Phone
                })
                .ToList();
            return View(veterinarios);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
