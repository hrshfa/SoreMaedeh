using Microsoft.AspNetCore.Identity;

namespace IbulakStoreServer.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
