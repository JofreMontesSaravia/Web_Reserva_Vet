using System;
using Microsoft.EntityFrameworkCore;
using Web_Vet_Pet.Data;
using Web_Vet_Pet.Interfaces;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> GetByPetIdAsync(int petId)
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .Where(a => a.PetId == petId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByVeterinarianIdAsync(int veterinarianId)
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .Where(a => a.VeterinarianId == veterinarianId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByServiceIdAsync(int serviceId)
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .Where(a => a.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByDateAsync(DateOnly date)
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .Where(a => a.DateBooking == date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetByStatusAsync(string status)
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .Where(a => a.StatusBooking == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdWithRelationsAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Pet)
                .Include(a => a.Service)
                .Include(a => a.Veterinarian)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
