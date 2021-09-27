using Microsoft.AspNetCore.Identity;

namespace IranTimes.Models
{
    public class ApplicationUser:IdentityUser
    {
        public bool IsPayed { get; set; }
    }
}
