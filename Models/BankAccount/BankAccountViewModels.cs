using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public class BankAccountEditViewModel
    {
        // BankAccount Id
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
    }
}