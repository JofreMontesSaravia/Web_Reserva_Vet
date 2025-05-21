namespace Web_Vet_Pet.Models
{
    public class Veterinarian
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Speciality { get; set; }

        //Propiedad de enlace entre tablas - citas
        public List<Appointment> Appointments { get; set; }
    }
}
