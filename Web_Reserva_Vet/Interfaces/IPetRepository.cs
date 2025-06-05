using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IPetRepository : IRepository<Pet>
    {
        Task<IEnumerable<Pet>> GetByClientIdAsync(int clientId); //muestra por id de cliente especifico
        Task<IEnumerable<Pet>> GetByTypePetIdAsync(int typePetId); //muestra por id de tipo de mascota especifico
        Task<IEnumerable<Pet>> GetWithAppointmentsAsync(); //devuelve mascota con sus citas
        Task<IEnumerable<Pet>> GetAllWithRelationsAsync(); //muestra todas las mascota + sus relaciones
        Task<Pet?> GetByIdWithRelationsAsync(int id); //devuelve una sola mascota, pero más sus relaciones
    }
}
