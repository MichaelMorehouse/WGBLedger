using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WGBLedger.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WGBLedger.DAL
{
    public class LedgerContext : IdentityDbContext<ApplicationUser>
    {
        public LedgerContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static LedgerContext Create()
        {
            return new LedgerContext();
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}