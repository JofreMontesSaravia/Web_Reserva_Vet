namespace Web_Vet_Pet.Models
{
    public class Veterinarian : Person
    {
        public string Specialty { get; set; }

        // Navegación uno a muchos (un veterinario tiene muchas citas)
        public ICollection<Appointment> Appointments { get; set; }
    }
}
