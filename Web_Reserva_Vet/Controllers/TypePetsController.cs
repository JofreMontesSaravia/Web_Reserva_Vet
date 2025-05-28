using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Controllers
{
    public class TypePetsController : Controller
    {
        private readonly ITypePetRepository _typePetRepository;

        public TypePetsController(ITypePetRepository typePetRepository)
        {
            _typePetRepository = typePetRepository;
        }

        // GET: TypePets
        public async Task<IActionResult> Index()
        {
            var typePets = await _typePetRepository.GetAllAsync();
            return View(typePets);
        }

        // GET: TypePets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var typePet = await _typePetRepository.GetByIdAsync(id.Value);
            if (typePet == null) return NotFound();

            return View(typePet);
        }

        // GET: TypePets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypePets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Species,Description")] TypePet typePet)
        {
            if (ModelState.IsValid)
            {
                await _typePetRepository.AddAsync(typePet);
                return RedirectToAction(nameof(Index));
            }
            return View(typePet);
        }

        // GET: TypePets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var typePet = await _typePetRepository.GetByIdAsync(id.Value);
            if (typePet == null) return NotFound();

            return View(typePet);
        }

        // POST: TypePets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Species,Description")] TypePet typePet)
        {
            if (id != typePet.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _typePetRepository.UpdateAsync(typePet);
                return RedirectToAction(nameof(Index));
            }
            return View(typePet);
        }

        // GET: TypePets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var typePet = await _typePetRepository.GetByIdAsync(id.Value);
            if (typePet == null) return NotFound();

            return View(typePet);
        }

        // POST: TypePets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _typePetRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: TypePets/BySpecies
        public async Task<IActionResult> BySpecies(string species)
        {
            if (string.IsNullOrWhiteSpace(species))
                return RedirectToAction(nameof(Index));

            var results = await _typePetRepository.GetBySpeciesAsync(species);
            return View("Index", results);
        }

        // GET: TypePets/WithPets
        public async Task<IActionResult> WithPets()
        {
            var list = await _typePetRepository.GetWithPetsAsync();
            return View("Index", list);
        }
    }
}
