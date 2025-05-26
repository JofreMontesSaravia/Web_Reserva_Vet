using System.ComponentModel.DataAnnotations;

namespace Web_Vet_Pet.Models
{
    public class Appointment
    {
        public int Id { get; set; } //Id de la reserva / cita
        public int PetId {  get; set; } //Fk mascota
        public int ServiceId { get; set; } //Fk Servicio
        public int VeterinarianId { get; set; } //Fk Veterinario

        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateBooking { get; set; }
        [Required]
        [StringLength(10)]
        public string? StatusBooking { get; set; }

        //Propiedad de navegacion
        public Service? Service {  get; set; }
        public Veterinarian? Veterinarian { get; set; }
        public Pet? Pet { get; set; }
    }
}
