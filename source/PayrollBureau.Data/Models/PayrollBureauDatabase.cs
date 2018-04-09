using System.Data.Entity;
using PayrollBureau.Data.Entities;

namespace PayrollBureau.Data.Models
{
    public partial class PayrollBureauDatabase : DbContext
    {
        public PayrollBureauDatabase() : base("name=OrbTalkDatabase")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserBureau> AspNetUserBureaux { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserEmployer> AspNetUserEmployers { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Bureau> Bureaux { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<DocumentGrid> DocumentGrids { get; set; }
        public virtual DbSet<EmployeeGrid> EmployeeGrids { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                 .HasMany(e => e.AspNetUserBureaus)
                 .WithRequired(e => e.AspNetUser)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserEmployers)
                .WithRequired(e => e.AspNetUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bureau>()
                .HasMany(e => e.AspNetUserBureaus)
                .WithRequired(e => e.Bureau)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bureau>()
                .HasMany(e => e.Employers)
                .WithRequired(e => e.Bureau)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeDocuments)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employer>()
                .HasMany(e => e.AspNetUserEmployers)
                .WithRequired(e => e.Employer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employer>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Employer)
                .WillCascadeOnDelete(false);

        }
    }
}


