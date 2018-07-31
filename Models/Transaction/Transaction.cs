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

        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$", ErrorMessage = "Invalid amount, please enter positive dollar amount")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Amount")]
        public double SignedAmount { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; set; }

        [DataType(DataType.Currency)]
        public double PreviousBalance { get; set; }
        
        // Navigation properties
        [Display(Name="Bank Account")]
        public virtual BankAccount BankAccount { get; set; }

    }
}