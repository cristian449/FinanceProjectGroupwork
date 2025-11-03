using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class RegisterViewmodel
    {

        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(8)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Confirmpassword { get; set; }

    }

}
