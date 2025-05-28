using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetWithClientsAsync()
        {
            return await _dbSet
                .Include(u => u.Clients)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetWithAdministratorsAsync()
        {
            return await _dbSet
                .Include(u => u.Administrators)
                .ToListAsync();
        }
    }
}
