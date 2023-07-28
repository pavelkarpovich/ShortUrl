using Microsoft.AspNetCore.Identity;

namespace ShortUrl.ApplicationCore.Entities
{
    public class Account : IdentityUser
    {
        public string Name { get; set; }
    }
}
