using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class VeterinarianRepository : Repository<Veterinarian>, IVeterinarianRepository
    {
        public VeterinarianRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Veterinarian>> GetBySpecialtyAsync(string specialty)
        {
            return await _dbSet
                .Where(v => v.Specialty == specialty)
                .ToListAsync();
        }

        public async Task<IEnumerable<Veterinarian>> GetByShiftAsync(int shift)
        {
            return await _dbSet
                .Where(v => v.Shift == shift)
                .ToListAsync();
        }

        public async Task<IEnumerable<Veterinarian>> GetWithAppointmentsAsync()
        {
            return await _dbSet
                .Include(v => v.Appointments)
                .ToListAsync();
        }

        public async Task<Veterinarian?> GetByIdWithAppointmentsAsync(int id)
        {
            return await _dbSet
                .Include(v => v.Appointments)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
    }
}
