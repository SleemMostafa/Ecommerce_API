using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Models
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() :base()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {

        }
        public DbSet<Category> categories { set; get; }
        public DbSet<Product>  Products { set; get; }

    }
}
