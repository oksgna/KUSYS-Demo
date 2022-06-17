using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo.WebUI.Identity
{
    public class ApplicationIdentityDbContext: IdentityDbContext<ApplicationUser>

    {

        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : base(options)
        {

        }

    }
}
