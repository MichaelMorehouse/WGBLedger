using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WGBLedger.Models
{
    public class BankAccountEditViewModel
    {
        // BankAccount Id
        [Display(Name="Account Number")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name="Account Created")]
        public DateTimeOffset DateCreated { get; set; } 

        [Display(Name="Account Type")]
        public AccountType AccountType { get; set; }
    }
}