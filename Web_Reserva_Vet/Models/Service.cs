using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web_Vet_Pet.Models
{
    public class Service
    {
        public int Id { get; set; } //PK de la entidad servicio
        [Required] //requerido si o si
        [StringLength(25)]
        public string Name { get; set; } //nombre
        [Required]
        [StringLength(45)]
        public string Description { get; set; } //descripcion
        [Required]
        [Range(0.01, 600.0)]
        public float Cost { get; set; } //costo
        [Required]
        [Range(1, 100)]
        public int Duration { get; set; } //duracion en minutos

        //Propiedad de Navegación
        [ValidateNever] //No valida su relación con citas para ser creado
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
