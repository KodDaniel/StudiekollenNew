using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StudiekollenNew.Models
{


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

            
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
            MapEntities(modelBuilder);
        }

        private void MapEntities(DbModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");


            //TestTable
            //modelBuilder.Entity<TestTable>()
            //    .HasRequired(c => c.User)
            //    .WithMany()
            //    .HasForeignKey(c => c.UserId);
                

            // QuestionTable
            modelBuilder.Entity<QuestionTable>().Property(e => e.Id).HasColumnName("QuestionId");
            modelBuilder.Entity<QuestionTable>()
                .HasRequired(c => c.TestTable)
                .WithMany()
                .HasForeignKey(c => c.TestId);
                
        }


        #region Initierar tabeller
        public DbSet<QuestionTable> QuestionTable { get; set; }
        public DbSet<TestTable> TestTable { get; set; }
        //public DbSet<User> User { get; set; } 
        #endregion

        public System.Data.Entity.DbSet<StudiekollenNew.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}