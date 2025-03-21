using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceLiveEfCore.Models;
using ProgettoBackend_S6_L5.Models;

namespace ProgettoBackend_S6_L5.Data
{
    public class ProgettoBackendDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ProgettoBackendDbContext(DbContextOptions<ProgettoBackendDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<Camera> Camere { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.ApplicationUserRole)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.ApplicationUserRole)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Prenotazione>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Prenotazioni)
            .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Prenotazione>()
            .HasOne(p => p.Camera)
            .WithMany(c => c.Prenotazioni)
            .HasForeignKey(p => p.CameraId);
        }
    }
}
