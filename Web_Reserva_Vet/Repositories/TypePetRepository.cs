using Microsoft.EntityFrameworkCore;
using System;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class TypePetRepository : Repository<TypePet>, ITypePetRepository
    {
        public TypePetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TypePet>> GetBySpeciesAsync(string species)
        {
            return await _dbSet
                .Where(tp => tp.Species == species)
                .ToListAsync();
        }

        public async Task<IEnumerable<TypePet>> GetWithPetsAsync()
        {
            return await _dbSet
                .Include(tp => tp.Pets)
                .ToListAsync();
        }
    }
}
