using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class RegisterViewmodel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string DateOfBirth { get; set; } //For now leavin it as string may change to Datetime later
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
