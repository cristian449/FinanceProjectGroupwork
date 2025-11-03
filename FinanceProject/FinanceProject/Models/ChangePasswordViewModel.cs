using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password: ")]
        public string CurrentPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password: ")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password: ")]
        public string ConfirmPassword { get; set; }

    }
}
