using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.DTOs;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;
using Web_Vet_Pet.Services;

namespace Web_Vet_Pet.Controllers
{
    public class AdministratorsController : Controller
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IValidacionUsers _usuarioService;

        public AdministratorsController(IAdministratorRepository administratorRepository, IValidacionUsers usuarioService)
        {
            _administratorRepository = administratorRepository;
            _usuarioService = usuarioService;
        }

        // GET: /Administrators
        public async Task<IActionResult> Index()
        {
            var administrators = await _administratorRepository.GetAllWithUsersAsync();
            return View(administrators);
        }

        // GET: /Administrators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _administratorRepository.GetByIdWithUsersAsync(id.Value);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // GET: /Administrators/Create
        public async Task<IActionResult> Create()
        {
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email");
            return View();
        }

        // POST: /Administrators/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                await _administratorRepository.AddAsync(administrator);
                return RedirectToAction(nameof(Index));
            }
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email", administrator.UserId);
            return View(administrator);
        }

        // GET: /Administrators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _administratorRepository.GetByIdWithUsersAsync(id.Value);
            if (administrator == null)
            {
                return NotFound();
            }
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email", administrator.UserId);
            return View(administrator);
        }

        // POST: /Administrators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Administrator administrator)
        {
            if (id != administrator.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingAdministrator = await _administratorRepository.GetByIdWithUsersAsync(id);
                if (existingAdministrator == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades para evitar conflictos de seguimiento
                existingAdministrator.UserId = administrator.UserId;

                try
                {
                    await _administratorRepository.UpdateAsync(existingAdministrator);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AdministratorExistsAsync(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email", administrator.UserId);
            return View(administrator);
        }

        // GET: /Administrators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrator = await _administratorRepository.GetByIdWithUsersAsync(id.Value);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }

        // POST: /Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrator = await _administratorRepository.GetByIdWithUsersAsync(id);
            if (administrator != null)
            {
                await _administratorRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Administrators/ByUserId/5
        public async Task<IActionResult> ByUserId(int userId)
        {
            var administrators = await _administratorRepository.GetByUserIdAsync(userId);
            return View(administrators);
        }

        private async Task<bool> AdministratorExistsAsync(int id)
        {
            var administrator = await _administratorRepository.GetByIdAsync(id);
            return administrator != null;
        }
    }
}
