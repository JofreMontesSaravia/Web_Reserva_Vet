using Web_Vet_Pet.Models;
using System.Threading.Tasks;

namespace Web_Vet_Pet.Interfaces
{
    //definimos la firma de los métodos especificos para veterinarios
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByPetIdAsync(int petId); //obtiene todas las citas respecto a un id de una mascota
        Task<IEnumerable<Appointment>> GetByVeterinarianIdAsync(int veterinarianId); //obtiene todas las citas respecto a un veterinario
        Task<IEnumerable<Appointment>> GetByServiceIdAsync(int serviceId); //obtiene todas las citas citas respecto a un servicio
        Task<IEnumerable<Appointment>> GetByDateAsync(DateOnly date); //obtiene citas por fecha especifica
        Task<IEnumerable<Appointment>> GetByStatusAsync(string status); //obtiene citas filtradas por estado
        Task<IEnumerable<Appointment>> GetAllWithRelationsAsync(); //devuelve todas las citas, incluyendo sus relaciones
        Task<Appointment?> GetByIdWithRelationsAsync(int id); //devuelve una cita con su información correspondiente
    }
}
