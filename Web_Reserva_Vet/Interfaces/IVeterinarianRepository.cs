using System.Threading.Tasks;
using Web_Vet_Pet.Models;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IVeterinarianRepository : IRepository<Veterinarian>
    {
        Task<IEnumerable<Veterinarian>> GetBySpecialtyAsync(string specialty); //muestra filtrado por especialidad
        Task<IEnumerable<Veterinarian>> GetByShiftAsync(int shift); //muestra filtrado por jornada
        Task<IEnumerable<Veterinarian>> GetWithAppointmentsAsync(); //muestra relacionado con cita
        Task<Veterinarian?> GetByIdWithAppointmentsAsync(int id); //muestra con un id de cita especifico
    }
}
