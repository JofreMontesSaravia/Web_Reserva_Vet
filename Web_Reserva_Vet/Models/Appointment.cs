namespace Web_Vet_Pet.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateOnly dateBooking { get; set; }
        public string statusAppointment { get; set; }

         
        //clave foránea - enlace con veterinario
        public int VeterinarianId { get; set; }
        // Propiedad de navegación (muchas citas -> un veterinario)
        public Veterinarian Veterinarian { get; set; }

        //clave foránea - enlace con cita
        public int ServiceId {  get; set; }
        // Propiedad de navegación (muchas citas -> un servicio)
        public Service Service { get; set; }

        //clave foránea - enlace con mascota
        public int PetId { get; set; }
        //Propiedad de navegación (muchas citas -> una mascota)
        public Pet Pet { get; set; }

    }
}
