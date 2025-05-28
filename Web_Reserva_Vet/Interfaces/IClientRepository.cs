using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Client>> GetWithPetsAsync();
        Task<IEnumerable<Client>> GetAllWithUsersAsync();
        Task<Client?> GetByIdWithUsersAsync(int id);
    }
}
