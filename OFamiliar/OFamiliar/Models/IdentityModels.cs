using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OFamiliar.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        // dados referentes 'a BD do projeto
        //Representar um 'construtor' desta classe
        //Representa onde se encontra a Base de Dados
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        //Descreve as tabelas que estao na DB
        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<Convite> Convites { get; set; }
        public virtual DbSet<Familia> Familias { get; set; }
        public virtual DbSet<Movimentos> Movimentos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // criar a FK para a tabela dos Convites,
            // relacionando os 'destinatários' com um 'convite'
            modelBuilder.Entity<Convite>()
                       .HasRequired(c => c.Destinatario)
                       .WithMany(p => p.ListaConvitesRecebidos)
                       .HasForeignKey(c => c.DestinatarioFK)
                       .WillCascadeOnDelete(false);

            // criar a FK para a tabela dos Convites,
            // relacionando os 'emissores' com um 'convite'
            modelBuilder.Entity<Convite>()
                       .HasRequired(c => c.Emissor)
                       .WithMany(p => p.ListaConvitesEmitidos)
                       .HasForeignKey(c => c.EmissorFK)
                       .WillCascadeOnDelete(false);
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }




    }
}