using Microsoft.AspNetCore.Identity;


namespace FinanceProject.Models
{
    public class User : IdentityUser<Guid>
    {
        //IsActive does duck all at the current moment
        public bool IsActive { get; set; }

        //Tracks the last time a user was active
        //Does Duck all at the current moment
        public DateTime? LastActive { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;



    }
}
