using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WGBLedger.Models;

namespace WGBLedger.Models
{
    public enum AccountType { Checking=0, Savings=1 }

    public class BankAccount
    {
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        [DataType(DataType.Currency)]
        public double Balance { get; set; }
        public Guid Id { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}