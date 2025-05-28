using System.Threading.Tasks;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Interfaces
{
    public interface IVeterinarianRepository : IRepository<Veterinarian>
    {
        Task<IEnumerable<Veterinarian>> GetBySpecialtyAsync(string specialty);
        Task<IEnumerable<Veterinarian>> GetByShiftAsync(int shift);
        Task<IEnumerable<Veterinarian>> GetWithAppointmentsAsync();
        Task<Veterinarian?> GetByIdWithAppointmentsAsync(int id);
    }
}
