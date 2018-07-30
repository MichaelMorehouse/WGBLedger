using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WGBLedger.Models;

namespace WGBLedger.Controllers
{
    public interface ITransaction
    {
        TransactionCreateViewModel SignTransactionAmount(TransactionCreateViewModel vm);
        Task<ActionResult> HandleTransaction(TransactionCreateViewModel vm);
    }
}