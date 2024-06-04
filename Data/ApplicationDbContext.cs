using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMobileRecharge.Models;

namespace OnlineMobileRecharge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Recharge> Recharges { get; set; }
        public DbSet<RechargeTransaction> RechargeTransactions { get; set; }
        public DbSet<CustomRechargeTransaction> CustomRechargeTransactions { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageTransaction> PackageTransactions { get; set; }
        public DbSet<CallerTune> CallerTunes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceTransaction> ServiceTransactions { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
    }
}
