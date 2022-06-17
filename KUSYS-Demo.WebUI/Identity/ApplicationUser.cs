using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.WebUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
