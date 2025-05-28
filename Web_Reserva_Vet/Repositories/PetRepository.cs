using Microsoft.EntityFrameworkCore;
using System;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pet>> GetByClientIdAsync(int clientId)
        {
            return await _dbSet
                .Include(p => p.Client)
                    .ThenInclude(c => c.Users)
                .Include(p => p.TypePet)
                .Where(p => p.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetByTypePetIdAsync(int typePetId)
        {
            return await _dbSet
                .Include(p => p.Client)
                    .ThenInclude(c => c.Users)
                .Include(p => p.TypePet)
                .Where(p => p.TypePetId == typePetId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetWithAppointmentsAsync()
        {
            return await _dbSet
                .Include(p => p.Appointments)
                .Include(p => p.Client)
                    .ThenInclude(c => c.Users)
                .Include(p => p.TypePet)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pet>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(p => p.Client)
                    .ThenInclude(c => c.Users)
                .Include(p => p.TypePet)
                .ToListAsync();
        }

        public async Task<Pet?> GetByIdWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Client)
                    .ThenInclude(c => c.Users)
                .Include(p => p.TypePet)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
