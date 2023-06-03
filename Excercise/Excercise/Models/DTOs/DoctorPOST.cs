using Excercise.Data;
using System.ComponentModel.DataAnnotations;

namespace Excercise.Models.DTOs
{
    public class DoctorPOST
    {
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
    }
}
