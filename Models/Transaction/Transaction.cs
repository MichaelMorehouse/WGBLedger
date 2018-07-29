using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public enum TransactionType { Withdrawal=0, Deposit=1 }
    public enum TransactionMethod { Cash=0, Check=1 }

    public class Transaction
    {
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        [Display(Name="Transaction Type")]
        public TransactionType TransactionType { get; set; }
        [Display(Name="Method")]
        public TransactionMethod TransactionMethod { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        [DataType(DataType.Currency)]
        public double PreviousBalance { get; set; }
        public Guid Id { get; set; }

        [Display(Name="Bank Account")]
        public virtual BankAccount BankAccount { get; set; }
    }
}