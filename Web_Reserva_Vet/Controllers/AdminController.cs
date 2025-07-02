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
using NuGet.Protocol.Plugins;
using Web_Vet_Pet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Web_Vet_Pet.Models;


namespace Web_Vet_Pet.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IUserRepository _userRepository;
        private readonly string[] protectedSections = { "Administrador" };
        public AdminController(IAdministratorRepository administratorRepository, IUserRepository userRepository)
        {
            _administratorRepository = administratorRepository;
            _userRepository = userRepository;
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
        public async Task<IActionResult> ValidatePassword(PasswordViewModel model)
        {
            // Obtener la contraseña del primer usuario admin registrado
            var user = await _administratorRepository.GetFirstAdminAsync();
            var user2=await _userRepository.GetByEmailAsync(user.Users.Email);


            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user2, user2.PasswordHash, model.Password);

            if (verificationResult == PasswordVerificationResult.Success)
            {
                return View("~/Views/Account/registerAdmin.cshtml");

            }
            else 
            {
                //JOFRE YA LE PONES TU EL MENSAJE DE ERROR no me da esa wea
                ModelState.AddModelError(nameof(model.Password), "La contraseña es incorrecta.");


            }

            return View("Administrador");

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

    }
}
