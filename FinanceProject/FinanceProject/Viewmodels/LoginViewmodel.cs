using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Viewmodels
{
    public class LoginViewmodel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
