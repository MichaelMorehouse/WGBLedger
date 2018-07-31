using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public class TransactionHistoryViewModel
    {
        public BankAccount BankAccount { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }

    public class TransactionCreateViewModel
    {
        [RegularExpression(@"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(\.[0-9][0-9])?$", ErrorMessage = "Invalid amount, please enter positive dollar amount")]
        public double Amount { get; set; }
        public double SignedAmount { get; set; }
        public string Description { get; set; }
        [Display(Name ="Transaction Type")]
        public TransactionType TransactionType { get; set; }
        [Display(Name="Bank Account")]
        public Guid BankAccount_Id { get; set; }
        public AccountType AccountType { get; set; }
    }

    public class TransactionEditViewModel
    {
        [Display(Name ="Transaction Id")]
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; set; }
        [Display(Name = "Bank Account")]
        public Guid BankAccount_Id { get; set; }
    }
}