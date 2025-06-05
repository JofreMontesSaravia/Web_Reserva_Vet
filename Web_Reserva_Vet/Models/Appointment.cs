using System.ComponentModel.DataAnnotations;

namespace Web_Vet_Pet.Models
{
    public class Appointment
    {
        public int Id { get; set; } //PK de la reserva / cita
        public int PetId {  get; set; } //Fk mascota
        public int ServiceId { get; set; } //Fk Servicio
        public int VeterinarianId { get; set; } //Fk Veterinario

        [Required] //requerido si o si
        [DataType(DataType.Date)]
        public DateOnly DateBooking { get; set; } //fecha de reserva
        [Required]
        [StringLength(10)]
        public string? StatusBooking { get; set; } //estado de reserva

        //Propiedad de navegacion
        public Service? Service {  get; set; }
        public Veterinarian? Veterinarian { get; set; }
        public Pet? Pet { get; set; }
    }
}
