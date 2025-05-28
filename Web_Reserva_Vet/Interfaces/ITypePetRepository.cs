using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    public interface ITypePetRepository : IRepository<TypePet>
    {
        Task<IEnumerable<TypePet>> GetBySpeciesAsync(string species);
        Task<IEnumerable<TypePet>> GetWithPetsAsync();
    }
}
