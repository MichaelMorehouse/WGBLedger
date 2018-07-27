using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public enum TransactionType { Deposit, Withdrawal }
    public enum TransactionMethod { Cash, Check }

    public class Transaction
    {
        public string Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionMethod TransactionMethod { get; set; }
        public Guid Id { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}