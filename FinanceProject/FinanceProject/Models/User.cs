using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace FinanceProject.Models
{
    public class User : IdentityUser<Guid>
    {
        //public Guid Id {  get; set; }
        //don't see the purpose of doing this but i'm kinda lost here
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
