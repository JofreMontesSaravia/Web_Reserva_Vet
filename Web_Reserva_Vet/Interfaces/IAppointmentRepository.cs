using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByPetIdAsync(int petId);
        Task<IEnumerable<Appointment>> GetByVeterinarianIdAsync(int veterinarianId);
        Task<IEnumerable<Appointment>> GetByServiceIdAsync(int serviceId);
        Task<IEnumerable<Appointment>> GetByDateAsync(DateOnly date);
        Task<IEnumerable<Appointment>> GetByStatusAsync(string status);
        Task<IEnumerable<Appointment>> GetAllWithRelationsAsync();
        Task<Appointment?> GetByIdWithRelationsAsync(int id);
    }
}
