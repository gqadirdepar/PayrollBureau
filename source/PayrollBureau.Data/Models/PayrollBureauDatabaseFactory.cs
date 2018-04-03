using System;
using PayrollBureau.Data.Interfaces;

namespace PayrollBureau.Data.Models
{
    public class PayrollBureauDatabaseFactory : IDatabaseFactory<PayrollBureauDatabase>
    {
        public string NameOrConnectionString { get; }

        public PayrollBureauDatabaseFactory(string nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString;
        }

        public PayrollBureauDatabase CreateContext()
        {
            ValidateConnectionString();
            var context = new PayrollBureauDatabase(NameOrConnectionString);
            return context;
        }

        private void ValidateConnectionString()
        {
            if (string.IsNullOrWhiteSpace(NameOrConnectionString))
                throw new NullReferenceException("PayrollBureauDatabaseFactory expects a valid NameOrConnectionString");
        }


    }
}
