using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        Task<IEnumerable<Administrator>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Administrator>> GetAllWithUsersAsync();
        Task<Administrator?> GetByIdWithUsersAsync(int id);
    }
}
