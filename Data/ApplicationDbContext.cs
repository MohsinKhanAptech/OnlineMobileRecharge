﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMobileRecharge.Models;

namespace OnlineMobileRecharge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Recharge> Recharges { get; set; }
        public DbSet<RechargeTransaction> Recharge_Transactions { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageTransaction> Package_Transactions { get; set; }
        public DbSet<CallerTune> Caller_Tunes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
    }
}
