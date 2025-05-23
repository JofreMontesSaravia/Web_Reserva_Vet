using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Controllers
{
    public class TypePetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypePetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TypePets
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypePets.ToListAsync());
        }

        // GET: TypePets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePet = await _context.TypePets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typePet == null)
            {
                return NotFound();
            }

            return View(typePet);
        }

        // GET: TypePets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypePets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Species,Description")] TypePet typePet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typePet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typePet);
        }

        // GET: TypePets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePet = await _context.TypePets.FindAsync(id);
            if (typePet == null)
            {
                return NotFound();
            }
            return View(typePet);
        }

        // POST: TypePets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Species,Description")] TypePet typePet)
        {
            if (id != typePet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typePet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypePetExists(typePet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(typePet);
        }

        // GET: TypePets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typePet = await _context.TypePets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typePet == null)
            {
                return NotFound();
            }

            return View(typePet);
        }

        // POST: TypePets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typePet = await _context.TypePets.FindAsync(id);
            if (typePet != null)
            {
                _context.TypePets.Remove(typePet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypePetExists(int id)
        {
            return _context.TypePets.Any(e => e.Id == id);
        }
    }
}
