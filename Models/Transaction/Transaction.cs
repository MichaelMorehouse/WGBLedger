using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public enum TransactionType { Withdrawal=0, Deposit=1 }

    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Amount")]
        public double SignedAmount { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        [DataType(DataType.Currency)]
        public double PreviousBalance { get; set; }
        
        // Navigation properties
        [Display(Name="Bank Account")]
        public virtual BankAccount BankAccount { get; set; }

    }
}