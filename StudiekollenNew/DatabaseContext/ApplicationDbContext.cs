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
            //modelBuilder.Entity<Test>().Property(t => t.Id).HasColumnName("ExamId");
            //modelBuilder.Entity<Test>().Property(t => t.ExamName).HasMaxLength(100).IsRequired();
            //modelBuilder.Entity<Test>().Property(t => t.CreateDate).IsOptional();
            //modelBuilder.Entity<Test>().Property(t => t.ChangeDate).IsOptional();




            //modelBuilder.Entity<Question>().Property(t => t.Id).HasColumnName("QuestionId");
            //modelBuilder.Entity<Question>().Property(t => t.Answer).IsOptional();
            //modelBuilder.Entity<Question>().Property(t => t.Result).HasMaxLength(20).IsOptional();


            //modelBuilder.Entity<Exam>().Property(e=>e.)


            //#region Relations
            //modelBuilder.Entity<Test>()
            //    .HasRequired(c => c.User)
            //    .WithMany(a => a.Test)
            //    .HasForeignKey(c => c.UserId)
            //    .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Question>()
            //    .HasRequired(c => c.Test)
            //    .WithMany(a => a.Questions)
            //    .HasForeignKey(c => c.ExamId)
            //    .WillCascadeOnDelete(true);
            //#endregion


   
            base.OnModelCreating(modelBuilder);
        }

       

    }
}