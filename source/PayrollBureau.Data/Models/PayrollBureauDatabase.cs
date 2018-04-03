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
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<Bureau> Bureaux { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<BureauGrid> BureauGrids { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Bureau>()
                .HasOptional(e => e.Employer)
                .WithRequired(e => e.Bureau);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeDocuments)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employer>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Employer)
                .WillCascadeOnDelete(false);
          
        }
    }
}


