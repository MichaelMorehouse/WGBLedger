﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public enum TransactionType { Deposit, Withdrawal }
    public enum TransactionMethod { Cash, Check }

    public class Transaction
    {
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Amount")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionMethod TransactionMethod { get; set; }
        public Guid Id { get; set; }

        public virtual BankAccount BankAccount { get; set; }
    }
}