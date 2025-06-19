using Microsoft.AspNetCore.Mvc;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.ViewModels;
using Web_Vet_Pet.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization; // Necesario para [AllowAnonymous]



namespace Web_Vet_Pet.Controllers
{
    public class AccountController: Controller
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

        [HttpGet]
        [AllowAnonymous] // Permite que cualquiera acceda a la página de login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userRepository.GetByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                return View(model);
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (verificationResult != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas.");
                return View(model);
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
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account"); // Redirigir al login después de cerrar sesión
        }

        // Añade estos métodos dentro de la clase AccountController

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido (ej. las contraseñas no coinciden),
                // regresa a la vista para mostrar los errores.
                return View(model);
            }

            // 1. VERIFICAR SI EL USUARIO YA EXISTE
            // Es crucial para evitar emails duplicados.
            bool userExists = await _userRepository.AnyAsync(u => u.Email == model.Email);
            if (userExists)
            {
                ModelState.AddModelError("Email", "Ya existe una cuenta registrada con este correo electrónico.");
                return View(model);
            }

            // 2. HASHEAR LA CONTRASEÑA
            // Usamos el mismo mecanismo seguro que en el login. ¡NUNCA guardes la contraseña en texto plano!
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(null, model.Password); // Pasamos null como user porque el hash no depende del usuario en sí

            // 3. CREAR EL NUEVO OBJETO User
            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                DateBirthday = model.DateBirthday,
                PasswordHash = hashedPassword // Guardamos el hash, no la contraseña real
            };

            // 4. GUARDAR EL NUEVO USUARIO EN LA BASE DE DATOS
            await _userRepository.AddAsync(newUser);
           

            // 5. ASIGNAR EL ROL DE "CLIENT" POR DEFECTO
            // Creamos una entrada en la tabla 'Clients' que se vincula con el nuevo 'User'.
            var newClient = new Client
            {
                UserId = newUser.Id // Usamos el ID que la BD acaba de generar para el usuario
            };

            await _clientRepository.AddAsync(newClient);
         

            // 6. REDIRIGIR AL USUARIO
            // Usamos TempData para mostrar un mensaje de éxito en la página de login.
            TempData["SuccessMessage"] = "¡Registro exitoso! Ahora puedes iniciar sesión.";

            return RedirectToAction("Login", "Account");
        }


    }
}
