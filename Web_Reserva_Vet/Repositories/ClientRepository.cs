using Microsoft.EntityFrameworkCore;
using System;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Client>> GetAllWithUsersAsync()
        {
            return await _dbSet
                .Include(c => c.Users)
                .ToListAsync();
        }

        public async Task<Client?> GetByIdWithUsersAsync(int id)
        {
            return await _dbSet.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Client>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(c => c.Users)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetWithPetsAsync()
        {
            return await _dbSet
                .Include(c => c.Users)
                .Include(c => c.Pets)
                .ToListAsync();
        }
    }
}
