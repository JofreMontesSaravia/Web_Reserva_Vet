using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web_Vet_Pet.Models
{
    public class Administrator
    {
        [Key] // Es buena práctica añadir [Key] también
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //PK Administrador
        public int UserId { get; set; } //Fk

        //Propiedad de navegación
        public User? Users { get; set; }
    }
}
