using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using WGBLedger.Models;

namespace WGBLedger.Controllers
{
    public interface ITransaction
    {
        Task<ActionResult> HandleTransaction(TransactionCreateViewModel vm);
    }
}