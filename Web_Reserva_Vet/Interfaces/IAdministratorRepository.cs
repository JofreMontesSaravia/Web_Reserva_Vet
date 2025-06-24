using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        Task<IEnumerable<Administrator>> GetByUserIdAsync(int userId); //obtener por id de usuairo
        Task<IEnumerable<Administrator>> GetAllWithUsersAsync(); //lista administradores con su relacion con user
        Task<Administrator?> GetByIdWithUsersAsync(int id); //obtener un solo administrador + relaciones
        Task<Administrator> GetFirstAdminAsync(); //contraseña del primer admin 
    }
}
