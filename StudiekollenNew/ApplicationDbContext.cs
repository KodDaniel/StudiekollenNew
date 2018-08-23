using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace StudiekollenNew.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("StudieContext", throwIfV1Schema: false)
        {
            //Disablar Lazy Loading eftersom det bör undvikas i webbapplikation
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Question> Question { get; set; }
        public DbSet<Test> Test{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {               
            #region Relations
            modelBuilder.Entity<Test>()
                .HasRequired(c => c.User)
                .WithMany(a => a.Test)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Question>()
                .HasRequired(c => c.Test)
                .WithMany(a => a.Questions)
                .HasForeignKey(c => c.TestId)
                .WillCascadeOnDelete(true);
            #endregion

            modelBuilder.Entity<Test>().Property(t => t.Id).HasColumnName("TestId");
            modelBuilder.Entity<Test>().Property(t => t.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Test>().Property(t => t.CreateDate).IsOptional();
            modelBuilder.Entity<Test>().Property(t => t.ChangeDate).IsOptional();

            
            modelBuilder.Entity<Question>().Property(t => t.Id).HasColumnName("QuestionId");
            modelBuilder.Entity<Question>().Property(t => t.Answer).IsOptional();
            modelBuilder.Entity<Question>().Property(t => t.Result).HasMaxLength(20).IsOptional();

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim").Property(p => p.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
            base.OnModelCreating(modelBuilder);
        }

       

    }
}