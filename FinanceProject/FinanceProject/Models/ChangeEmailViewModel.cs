using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class ChangeEmailViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "New email: ")]
        public string NewEmail { get; set; }
    }
}
