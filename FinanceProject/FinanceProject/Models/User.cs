using Microsoft.AspNetCore.Identity;


namespace FinanceProject.Models
{
    public class User : IdentityUser<Guid>
    {
        //Does duck all at the current moment
        public bool IsActive { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    }
}
