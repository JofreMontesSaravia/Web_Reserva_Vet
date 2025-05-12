namespace Web_Vet_Pet.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name_Service { get; set; }
        public string Description_Service { get; set; }
        public float Cost {  get; set; }
        public int Duration { get; set; }

        // Navegación uno a muchos (un servicio tiene muchas citas)
        public ICollection<Appointment> Appointments { get; set; }
    }
}
