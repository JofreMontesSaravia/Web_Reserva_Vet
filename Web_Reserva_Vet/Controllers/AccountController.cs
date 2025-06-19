using Microsoft.AspNetCore.Mvc;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.ViewModels; // <-- Asegúrate que use el namespace de ViewModels
using Web_Vet_Pet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Web_Vet_Pet.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdministratorRepository _adminRepository;
        private readonly IClientRepository _clientRepository;

        public AccountController(
            IUserRepository userRepository,
            IAdministratorRepository adminRepository,
            IClientRepository clientRepository)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _clientRepository = clientRepository;
        }

        // --- 1. MÉTODO GET PARA MOSTRAR LA PÁGINA ---
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var viewModel = new LoginAndRegisterViewModel();
            return View(viewModel);
        }

        // --- 2. MÉTODO POST PARA PROCESAR EL LOGIN ---
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAndRegisterViewModel viewModel)
        {
         
            var loginModel = viewModel.Login;

           
            if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
            {
                ModelState.AddModelError("Login.Email", "El correo y la contraseña no pueden estar vacíos.");
                return View("Login", viewModel); // Devolvemos la vista combinada
            }

            
            var user = await _userRepository.GetByEmailAsync(loginModel.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                
                return View("Login", viewModel);
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                return View("Login", viewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
            };

            if (await _adminRepository.AnyAsync(a => a.UserId == user.Id))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            if (await _clientRepository.AnyAsync(c => c.UserId == user.Id))
            {
                claims.Add(new Claim(ClaimTypes.Role, "Client"));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Ahora usamos el 'RememberMe' del modelo de login anidado
                IsPersistent = loginModel.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }


        // --- 3. MÉTODO POST PARA PROCESAR EL REGISTRO (También adaptado) ---
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(LoginAndRegisterViewModel viewModel)
        {

            // Le decimos al sistema que ignore los errores de los campos del Login
            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Login.")))
            {
                ModelState.Remove(key);
            }
            

            var registerModel = viewModel.Register;

            //Validamos que el usuario halla ingresado todo
            if (!ModelState.IsValid)
            {

                ViewData["ShowRegister"] = true;
                return View("Login", viewModel);
            }

           //validacions del sistemas

            bool userExists = await _userRepository.AnyAsync(u => u.Email == registerModel.Email);
            if (userExists)
            {
                ModelState.AddModelError("Register.Email", "Ya existe una cuenta con este correo.");
                ViewData["ShowRegister"] = true;
                return View("Login", viewModel);
            }

            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(null, registerModel.Password);

            var newUser = new User
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                Phone = registerModel.Phone,
                DateBirthday = registerModel.DateBirthday,
                PasswordHash = hashedPassword
            };

            var newClient = new Client { Users = newUser };

            await _userRepository.AddAsync(newUser);
            await _clientRepository.AddAsync(newClient);
       

            TempData["SuccessMessage"] = "¡Registro exitoso! Ahora puedes iniciar sesión.";
            return RedirectToAction("Login");
        }

        // --- 4. MÉTODO LOGOUT (No necesita cambios) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
