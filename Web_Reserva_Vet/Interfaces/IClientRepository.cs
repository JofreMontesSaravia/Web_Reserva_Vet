using Web_Vet_Pet.Models;
using System.Threading.Tasks;


namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetByUserIdAsync(int userId); //obtener por id de usuairo
        Task<IEnumerable<Client>> GetWithPetsAsync(); //lista de clientes más lista de mascotas
        Task<IEnumerable<Client>> GetAllWithUsersAsync(); //lista cliente con su relacion con user
        Task<Client?> GetByIdWithUsersAsync(int id); //obtener un solo cliente + relaciones
       
    }
}
