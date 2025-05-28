using System;
using Web_Vet_Pet.Data;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Administrator>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(a => a.Users)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Administrator>> GetAllWithUsersAsync()
        {
            return await _dbSet
                .Include(a => a.Users)
                .ToListAsync();
        }

        public async Task<Administrator?> GetByIdWithUsersAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Users)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
