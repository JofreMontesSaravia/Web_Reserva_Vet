using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace Web_Vet_Pet.Models
{
    public class Veterinarian
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Specialty { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        public string Email { get; set; }
        [Required]
        [Range(1,10)]
        public int Shift {  get; set; }
        [Required]
        [StringLength(9)]
        public string Phone {  get; set; }

        //Propiedad de navegacion
        [ValidateNever]
        public ICollection<Appointment> Appointments { get; set; }
    }
}
