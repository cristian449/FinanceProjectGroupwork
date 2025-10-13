using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace FinanceProject.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
