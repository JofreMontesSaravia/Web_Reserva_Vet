namespace Web_Vet_Pet.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public DateOnly Date_Birth { get; set; }
    }
}
