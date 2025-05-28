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
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidacionUsers _usuarioService;

        public ClientsController(IClientRepository clientRepository, IValidacionUsers usuarioService)
        {
            _clientRepository = clientRepository;
            _usuarioService = usuarioService;
        }

        // GET: /Clients
        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAllWithUsersAsync();
            return View(clients);
        }

        // GET: /Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdWithUsersAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: /Clients/Create
        public async Task<IActionResult> Create()
        {
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email");
            return View();
        }

        // POST: /Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientRepository.AddAsync(client);
                return RedirectToAction(nameof(Index));
            }
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email", client.UserId);
            return View(client);
        }

        // GET: /Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdWithUsersAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email", client.UserId);
            return View(client);
        }

        // POST: /Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingClient = await _clientRepository.GetByIdWithUsersAsync(id);
                if (existingClient == null)
                {
                    return NotFound();
                }

                // Actualizar propiedades para evitar conflictos de seguimiento
                existingClient.UserId = client.UserId;

                try
                {
                    await _clientRepository.UpdateAsync(existingClient);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ClientExistsAsync(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            var users = await _usuarioService.ObtenerSeleccionUsuarioDisponiblesync();
            ViewData["UserId"] = new SelectList(users, "Id", "email", client.UserId);
            return View(client);
        }

        // GET: /Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdWithUsersAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: /Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdWithUsersAsync(id);
            if (client != null)
            {
                await _clientRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Clients/ByUserId/5
        public async Task<IActionResult> ByUserId(int userId)
        {
            var clients = await _clientRepository.GetByUserIdAsync(userId);
            return View(clients);
        }

        // GET: /Clients/WithPets
        public async Task<IActionResult> WithPets()
        {
            var clients = await _clientRepository.GetWithPetsAsync();
            return View(clients);
        }

        private async Task<bool> ClientExistsAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return client != null;
        }
    }
}
