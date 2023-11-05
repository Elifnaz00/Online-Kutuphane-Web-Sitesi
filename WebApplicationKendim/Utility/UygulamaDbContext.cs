using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationKendim.Models;


namespace WebApplicationKendim.Utility
{
    public class UygulamaDbContext : IdentityDbContext

    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options)
            : base(options) 
            {
            
            }

        public DbSet<KitapTurleri> KitapTurleriTablosu { get; set; }
        public DbSet<Kitap> KitapBilgileriTablosu { get; set; }

        public DbSet<Kiralama> Kiralamalar { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}

    


