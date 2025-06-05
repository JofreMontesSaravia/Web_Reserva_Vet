using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface ITypePetRepository : IRepository<TypePet>
    {
        Task<IEnumerable<TypePet>> GetBySpeciesAsync(string species); //muestra por especie
        Task<IEnumerable<TypePet>> GetWithPetsAsync(); //muestra relacionado con mascota
    }
}
