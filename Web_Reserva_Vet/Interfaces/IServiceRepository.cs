using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetByCostRangeAsync(float minCost, float maxCost);
        Task<IEnumerable<Service>> GetWithAppointmentsAsync();
    }
}
