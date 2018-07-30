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
        public double Amount { get; set; }
        public double SignedAmount { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        [Display(Name="Bank Account")]
        public Guid BankAccount_Id { get; set; }
        public AccountType AccountType { get; set; }
    }
}