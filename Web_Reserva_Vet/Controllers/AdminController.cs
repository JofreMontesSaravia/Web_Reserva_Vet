using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.ViewModels;

namespace Web_Vet_Pet.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Name_Vet = "Clínica Veterinaria Patitas";
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
