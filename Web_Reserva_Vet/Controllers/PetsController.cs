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
using Web_Vet_Pet.Services;

namespace Web_Vet_Pet.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITypePetRepository _typePetRepository;
        private readonly IValidacionUsers _usuarioService;

        public PetsController(IPetRepository petRepository, IClientRepository clientRepository, ITypePetRepository typePetRepository, IValidacionUsers usuarioService)
        {
            _petRepository = petRepository;
            _clientRepository = clientRepository;
            _typePetRepository = typePetRepository;
            _usuarioService = usuarioService;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var pets = await _petRepository.GetAllWithRelationsAsync();
            return View(pets);
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pet = await _petRepository.GetByIdWithRelationsAsync(id.Value);
            if (pet == null) return NotFound();

            return View(pet);
        }

        // GET: Pets/Create
        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();
            return View();
        }

        // POST: Pets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,TypePetId,Name,Breed,Age")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                await _petRepository.AddAsync(pet);
                return RedirectToAction(nameof(Index));
            }

            await LoadSelectLists(pet.ClientId, pet.TypePetId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pet = await _petRepository.GetByIdWithRelationsAsync(id.Value);
            if (pet == null) return NotFound();

            await LoadSelectLists(pet.ClientId, pet.TypePetId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,TypePetId,Name,Breed,Age")] Pet pet)
        {
            if (id != pet.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPet = await _petRepository.GetByIdWithRelationsAsync(id);
                    if (existingPet == null) return NotFound();

                    // Actualizar propiedades para evitar conflictos de seguimiento
                    existingPet.ClientId = pet.ClientId;
                    existingPet.TypePetId = pet.TypePetId;
                    existingPet.Name = pet.Name;
                    existingPet.Breed = pet.Breed;
                    existingPet.Age = pet.Age;

                    await _petRepository.UpdateAsync(existingPet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PetExistsAsync(id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            await LoadSelectLists(pet.ClientId, pet.TypePetId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pet = await _petRepository.GetByIdWithRelationsAsync(id.Value);
            if (pet == null) return NotFound();

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _petRepository.GetByIdWithRelationsAsync(id);
            if (pet != null)
            {
                await _petRepository.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Pets/ByClientId/5
        public async Task<IActionResult> ByClientId(int clientId)
        {
            var pets = await _petRepository.GetByClientIdAsync(clientId);
            return View(pets);
        }

        // GET: Pets/ByTypePetId/5
        public async Task<IActionResult> ByTypePetId(int typePetId)
        {
            var pets = await _petRepository.GetByTypePetIdAsync(typePetId);
            return View(pets);
        }

        // GET: Pets/WithAppointments
        public async Task<IActionResult> WithAppointments()
        {
            var pets = await _petRepository.GetWithAppointmentsAsync();
            return View(pets);
        }

        private async Task<bool> PetExistsAsync(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            return pet != null;
        }

        private async Task LoadSelectLists(int? selectedClientId = null, int? selectedTypePetId = null)
        {
            var clients = await _clientRepository.GetAllWithUsersAsync();
            var typePets = await _typePetRepository.GetAllAsync();

            // Mostrar todos los clientes con usuarios válidos, sin filtro de usuarios disponibles
            var availableClients = clients
                .Where(c => c.Users != null)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"ID: {c.Id} - {c.Users.Email}"
                })
                .ToList();

            var typePetList = typePets?
                .Where(tp => !string.IsNullOrEmpty(tp.Description))
                .Select(tp => new SelectListItem
                {
                    Value = tp.Id.ToString(),
                    Text = tp.Description
                })
                .ToList() ?? new List<SelectListItem>();

            ViewData["ClientId"] = new SelectList(availableClients.Any() ? availableClients : new List<SelectListItem>(), "Value", "Text", selectedClientId);
            ViewData["TypePetId"] = new SelectList(typePetList.Any() ? typePetList : new List<SelectListItem>(), "Value", "Text", selectedTypePetId);
        }
    }
}
