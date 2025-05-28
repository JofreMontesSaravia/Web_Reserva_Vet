using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Repositories;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Controllers
{
    public class VeterinariansController : Controller
    {
        private readonly IVeterinarianRepository _veterinarianRepository;

        public VeterinariansController(IVeterinarianRepository veterinarianRepository)
        {
            _veterinarianRepository = veterinarianRepository;
        }

        // GET: /Veterinarians
        public async Task<IActionResult> Index()
        {
            var veterinarians = await _veterinarianRepository.GetWithAppointmentsAsync();
            return View(veterinarians);
        }

        // GET: /Veterinarians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarian = await _veterinarianRepository.GetByIdWithAppointmentsAsync(id.Value);
            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        // GET: /Veterinarians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Veterinarians/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Specialty,Name,Email,Shift,Phone")] Veterinarian veterinarian)
        {
            if (ModelState.IsValid)
            {
                await _veterinarianRepository.AddAsync(veterinarian);
                return RedirectToAction(nameof(Index));
            }
            return View(veterinarian);
        }

        // GET: /Veterinarians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarian = await _veterinarianRepository.GetByIdWithAppointmentsAsync(id.Value);
            if (veterinarian == null)
            {
                return NotFound();
            }
            return View(veterinarian);
        }

        // POST: /Veterinarians/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Specialty,Name,Email,Shift,Phone")] Veterinarian veterinarian)
        {
            if (id != veterinarian.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingVeterinarian = await _veterinarianRepository.GetByIdWithAppointmentsAsync(id);
                    if (existingVeterinarian == null)
                    {
                        return NotFound();
                    }

                    // Actualizar propiedades para evitar conflictos de seguimiento
                    existingVeterinarian.Specialty = veterinarian.Specialty;
                    existingVeterinarian.Name = veterinarian.Name;
                    existingVeterinarian.Email = veterinarian.Email;
                    existingVeterinarian.Shift = veterinarian.Shift;
                    existingVeterinarian.Phone = veterinarian.Phone;

                    await _veterinarianRepository.UpdateAsync(existingVeterinarian);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VeterinarianExistsAsync(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(veterinarian);
        }

        // GET: /Veterinarians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarian = await _veterinarianRepository.GetByIdWithAppointmentsAsync(id.Value);
            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        // POST: /Veterinarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veterinarian = await _veterinarianRepository.GetByIdWithAppointmentsAsync(id);
            if (veterinarian != null)
            {
                await _veterinarianRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Veterinarians/BySpecialty
        public async Task<IActionResult> BySpecialty(string specialty)
        {
            if (string.IsNullOrEmpty(specialty))
            {
                return BadRequest("La especialidad no puede estar vacía.");
            }

            var veterinarians = await _veterinarianRepository.GetBySpecialtyAsync(specialty);
            return View(veterinarians);
        }

        // GET: /Veterinarians/ByShift
        public async Task<IActionResult> ByShift(int shift)
        {
            var veterinarians = await _veterinarianRepository.GetByShiftAsync(shift);
            return View(veterinarians);
        }

        private async Task<bool> VeterinarianExistsAsync(int id)
        {
            var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
            return veterinarian != null;
        }
    }
}
