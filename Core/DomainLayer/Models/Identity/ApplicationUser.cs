using Microsoft.AspNetCore.Identity;
using DomainLayer.Models.Identity;

namespace DomainLayer.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public Address? Address { get; set; }
    }
}
