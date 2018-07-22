using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudiekollenNew.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
         //Obs: Det nedan inom markeringarna är antagligen inte god practice. Finns annan lösning? Just nu fyller det dock...
        //...sin funktion: nämligen att jag göra mina kopplingar längre nedan i denna fil vad gäller foregin keys och relationer.
        //--------------------------------------------------------------------
        public virtual TestTable TestTable { get; set; }
        public virtual ResultTable ResultTable { get; set; }
        public virtual QuestionTable QuestionTable{ get; set; }
        //--------------------------------------------------------------------------

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

     //Min DbContext. 
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
            //MapEntities(modelBuilder);
        }

        private void MapEntities(DbModelBuilder modelBuilder)
        {
            //Ett test kan tillhöra flera användare.
            modelBuilder.Entity<TestTable>()
                .HasRequired(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            // Fler eller en fråga/frågor kan tillhör ett prov
            modelBuilder.Entity<QuestionTable>()
                .HasRequired(c => c.TestTable)
                .WithMany()
                .HasForeignKey(c => c.TestId);
        }

        //public DbSet<OstTable>

        public DbSet<QuestionTable> QuestionTable { get; set; }
        public DbSet<TestTable> TestTable { get; set; }
        public DbSet<ResultTable> ResultTable { get; set; }

    }
}