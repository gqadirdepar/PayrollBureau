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
        public virtual DbSet<Bureau> Bureaus { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public virtual DbSet<Employer> Employers { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<EmployeeGrid> EmployeeGrids { get; set; }
        public virtual DbSet<DocumentGrid> DocumentGrids { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bureau>()
               .HasMany(e => e.Employers)
               .WithRequired(e => e.Bureau)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employer>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Employer)
                .WillCascadeOnDelete(false);
          
        }
    }
}


