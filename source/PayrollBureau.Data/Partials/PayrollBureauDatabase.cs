using System.Data.Entity;

namespace PayrollBureau.Data.Models
{
    /// Ensure the generated HRDatabase also references OrganisationDbContext
    /// and the OnModelCreating has the following as its last line of code:  base.OnModelCreating(modelBuilder);
    public partial class PayrollBureauDatabase : DbContext
    {
        public PayrollBureauDatabase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Initialise();
        }

        private void Initialise()
        {
            //Disable initializer
            Database.SetInitializer<PayrollBureauDatabase>(null);
            Database.CommandTimeout = 300;
            Configuration.ProxyCreationEnabled = false;
        }
        // Ensure this function is called with in the generated PayrollBureauDatabase
    }
}
