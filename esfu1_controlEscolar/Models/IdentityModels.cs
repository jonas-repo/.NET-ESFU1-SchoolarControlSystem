using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using esfu1_controlEscolar.Areas.Admins.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace esfu1_controlEscolar.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Calificacion> Calificacion { get; set; }

        public virtual DbSet<DocumentosFaltantes> DocumentosFaltantes { get; set; }
        public virtual DbSet<Avisos> Avisos { get; set; }
        public virtual DbSet<Reporte> Reporte { get; set; }
        public virtual DbSet<Extraordinarios> Extraordinarios { get; set; }
        public virtual DbSet<Citatorio> Citatorio { get; set; }
    }
}