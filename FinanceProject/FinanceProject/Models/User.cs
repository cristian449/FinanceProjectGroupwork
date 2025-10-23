using Microsoft.AspNetCore.Identity;


namespace FinanceProject.Models
{
    public class User : IdentityUser<Guid>
    {

        public bool IsActive { get; set; }
    }
}
