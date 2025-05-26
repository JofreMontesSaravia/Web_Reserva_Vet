using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Web_Vet_Pet.Models
{
    public class Service
    {
        public int Id { get; set; } //Id Servicio
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(45)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, 600.0)]
        public float Cost { get; set; }
        [Required]
        [Range(1, 100)]
        public int Duration { get; set; }

        //Propiedad de Navegación
        [ValidateNever]
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
