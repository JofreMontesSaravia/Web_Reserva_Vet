using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetByCostRangeAsync(float minCost, float maxCost); //muestra por rango de costo 
        Task<IEnumerable<Service>> GetWithAppointmentsAsync(); //muestra relacionado por cita
    }
}
