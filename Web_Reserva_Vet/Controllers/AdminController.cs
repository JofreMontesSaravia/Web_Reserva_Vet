using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_Reserva_Vet.Models;
using Web_Reserva_Vet.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Repositories;

namespace Web_Vet_Pet.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly string[] protectedSections = { "Administrador" };
        public AdminController(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }
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
        public IActionResult Administrador()
        {
            ViewBag.ShowPasswordForm = true;
            ViewData["Titulo"] = "Administrador";
            ViewData["Icono"] = "fa-solid fa-user-tie";
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> ValidatePassword(string password, string returnUrl)
        {
            // Obtener la contrase�a del primer usuario admin registrado
            var firstAdmin = await _administratorRepository.GetFirstAdminAsync();

            if (firstAdmin != null)
            {
                // Validar contrase�a (en un entorno real, usa PasswordHasher)
                if (VerifyPassword(password, firstAdmin.Users.PasswordHash)) // Implementa esta funci�n
                {
                    HttpContext.Session.SetString("AdminAuthenticated", "true");
                    return RedirectToAction(returnUrl);
                }
            }

            ViewBag.ShowPasswordForm = true;
            ViewBag.ErrorMessage = "Contrase�a incorrecta o usuario no encontrado. Int�ntalo de nuevo.";
            return View(returnUrl);
        }
        private IActionResult RequirePassword(string section, string icon)
        {
            if (protectedSections.Contains(section))
            {
                ViewBag.ShowPasswordForm = true;
                ViewBag.ErrorMessage = null;
                return View(section);
            }
            else
            {
                ViewData["Titulo"] = section;
                ViewData["Icono"] = icon;
                return View(section);
            }
        }

        // M�todo auxiliar para verificar contrase�a (simplificado, usa PasswordHasher en producci�n)
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            // En un entorno real, usa un hasher como Microsoft.AspNetCore.Identity.PasswordHasher
            // Aqu� asumimos que storedHash es la contrase�a en texto plano para simplicidad
            return inputPassword == storedHash; // Reemplaza con l�gica de hashing
        }
    }
}
