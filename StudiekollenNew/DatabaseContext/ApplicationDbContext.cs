using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using StudiekollenNew.DomainModels;
using StudiekollenNew.ViewModels;

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

        public DbSet<Exam> Exam { get; set; }
        public DbSet<Question> Question{ get; set; }

        //public DbSet<ExamStats> ExamStats { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Exam>().Property(t => t.ExamName).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Exam>().Property(t => t.CreateDate).IsRequired();
            modelBuilder.Entity<Exam>().Property(t => t.CreateDate).IsRequired();
            modelBuilder.Entity<Exam>().Property(d => d.CreateDate).HasColumnType("datetime2");
            modelBuilder.Entity<Exam>().Property(d => d.ChangeDate).HasColumnType("datetime2");



            modelBuilder.Entity<Exam>()
                .HasRequired(c => c.User)
                .WithMany(a => a.Exams)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<Question>()
                .HasRequired(c => c.Exam)
                .WithMany(a => a.Questions)
                .HasForeignKey(c => c.ExamId)
                .WillCascadeOnDelete(true);



            base.OnModelCreating(modelBuilder);
        }

       

    }
}