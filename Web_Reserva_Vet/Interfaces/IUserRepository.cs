using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email); //muestra por un email
        Task<IEnumerable<User>> GetWithClientsAsync(); //muestra relacionado con cliente
        Task<IEnumerable<User>> GetWithAdministratorsAsync(); //muestra relacionado con administrador
        
    }
}
