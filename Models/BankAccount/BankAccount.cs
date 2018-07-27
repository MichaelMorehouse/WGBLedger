using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WGBLedger.Models;

namespace WGBLedger.Models
{
    public enum AccountType { Checking, Savings }

    public class BankAccount
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        public Guid Id { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}