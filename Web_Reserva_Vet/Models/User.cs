using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;




namespace Web_Vet_Pet.Models
{
    public class User
    {

        [Key] // Es buena práctica añadir [Key] también
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //PK de la tabla
        [Required] //requerido si o si
        [StringLength(20)]
        public string? FirstName { get; set; } //primer normbre
        [Required]
        [StringLength(30)]
        public string? Email { get; set; } //correo
        [Required]
        [StringLength(256)]
        public string? PasswordHash { get; set; } //contraseña
        [Required]
        [StringLength(20)]
        public string? LastName { get; set; } //apellido
        [Required]
        [StringLength(9)]
        public string? Phone { get; set; } //telefono
        public DateOnly DateBirthday { get; set; } //cumpleaños

        //Propiedad de navegación
        [ValidateNever] //no valida su relación con cliente al ser creado
        public ICollection<Client>? Clients { get; set; }
        [ValidateNever] //no valida su relación con admin al ser creado
        public ICollection<Administrator>? Administrators { get; set; }
    }
}
