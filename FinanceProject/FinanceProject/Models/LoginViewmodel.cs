using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class LoginViewmodel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        // Add a way later for the user to be remember for easier login

    }
}
