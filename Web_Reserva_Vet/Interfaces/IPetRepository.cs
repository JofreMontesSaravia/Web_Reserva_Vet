using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetByClientIdAsync(int clientId);
        Task<IEnumerable<Pet>> GetByTypePetIdAsync(int typePetId);
        Task<IEnumerable<Pet>> GetWithAppointmentsAsync();
        Task<IEnumerable<Pet>> GetAllWithRelationsAsync();
        Task<Pet?> GetByIdWithRelationsAsync(int id);
    }
}
