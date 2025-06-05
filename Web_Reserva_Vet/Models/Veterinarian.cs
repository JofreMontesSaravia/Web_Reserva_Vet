using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Web_Vet_Pet.Models
{
    public class Veterinarian
    {
        public int Id { get; set; } //PK de la entidad
        [Required] //requerido si o si
        [StringLength(25)]
        public string Specialty { get; set; } //especialidad
        [Required]
        [StringLength(20)]
        public string Name { get; set; } //nombre
        [Required]
        [StringLength(25)]
        public string Email { get; set; } //correo
        [Required]
        [Range(1,10)]
        public int Shift {  get; set; } //jornada laboral
        [Required]
        [StringLength(9)]
        public string Phone {  get; set; } //celular

        //Propiedad de navegacion
        [ValidateNever] //No valida su relacion con citas para ser creado
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
