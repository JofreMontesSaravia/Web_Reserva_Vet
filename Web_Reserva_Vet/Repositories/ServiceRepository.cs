using Microsoft.EntityFrameworkCore;
using System;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Service>> GetByCostRangeAsync(float minCost, float maxCost)
        {
            return await _dbSet
                .Where(s => s.Cost >= minCost && s.Cost <= maxCost)
                .ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetWithAppointmentsAsync()
        {
            return await _dbSet
                .Include(s => s.Appointments)
                .ToListAsync();
        }
    }
}
