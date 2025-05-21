namespace Web_Vet_Pet.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name_Service { get; set; }
        public string Description_Service { get; set; }
        public float Cost {  get; set; }
        public int Duration { get; set; }

        //Propiedad de enlace entre tablas - citas
        public List<Appointment> Appointments { get; set; }
    }
}
