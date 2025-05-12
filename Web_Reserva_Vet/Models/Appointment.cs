namespace Web_Vet_Pet.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // Relación con Pet
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        // Relación con Service
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        // Relación con Veterinarian (puede ser null)
        public int? VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }

        public DateOnly DateReservation { get; set; }
        public string Status { get; set; }
    }
}
