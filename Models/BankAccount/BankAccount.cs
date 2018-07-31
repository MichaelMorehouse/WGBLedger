using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WGBLedger.Models;

namespace WGBLedger.Models
{
    public enum AccountType { Debit=0, Credit=1 }

    public class BankAccount
    {
        [Display(Name = "Account Number")]
        public Guid Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [Display(Name="Account Created")]
        public DateTimeOffset DateCreated { get; set; }

        [Display(Name="Type")]
        public AccountType AccountType { get; set; }

        [DataType(DataType.Currency)]
        public double Balance { get; set; }


        // Navigation properties
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}