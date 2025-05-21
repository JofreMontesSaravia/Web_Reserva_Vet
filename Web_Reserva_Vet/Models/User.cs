namespace Web_Vet_Pet.Models
{
    public class User
    {
        // variables que serán componentes de la tabla
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public DateOnly Date_Birth { get; set; } //fecha sin hora

        // Propiedades para enlace de tablas
        // - uno a uno (usuario - cliente)
        public Client Clients { get; set; }
        // - uno a uno (usuario - administrador)
        public Administrator administrators { get; set; }
    }
}
