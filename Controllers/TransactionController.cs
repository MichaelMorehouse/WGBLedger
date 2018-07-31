using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WGBLedger.Models;

namespace WGBLedger.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transaction
        public async Task<ActionResult> Index(Guid? acctId)
        {
            if (acctId == null)
            {
                return RedirectToAction("Index", "BankAccount");
            }
            var vm = await PopulateTransactionHistoryViewModelAsync((Guid)acctId);
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Transaction/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transaction/Create
        public async Task<ActionResult> Create(Guid? acctId)
        {
            if (acctId == null)
            {
                return RedirectToAction("Index","BankAccount");
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(acctId);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            TransactionCreateViewModel transaction = new TransactionCreateViewModel();
            transaction.BankAccount_Id = (Guid)acctId;
            transaction.AccountType = bankAccount.AccountType;
            ViewBag.Name = bankAccount.Name;
            return View(transaction);
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TransactionCreateViewModel vm)
        {
            BankAccount bankAccount = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == vm.BankAccount_Id);

            if (bankAccount == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                SignTransactionAmount(vm);

                Transaction transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Date = DateTimeOffset.Now,
                    BankAccount = bankAccount,
                    PreviousBalance = bankAccount.Balance,
                    Amount = vm.Amount,
                    SignedAmount = vm.SignedAmount,
                    Description = vm.Description,
                    TransactionType = vm.TransactionType,
                };
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                await HandleTransactionAsync(vm);
                return RedirectToAction("Index", "Transaction", new { acctId = vm.BankAccount_Id });
            }

            return View(vm.BankAccount_Id);
        }

        // GET: Transaction/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            TransactionEditViewModel vm = new TransactionEditViewModel
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Date = transaction.Date,
                TransactionType = transaction.TransactionType,
                BankAccount_Id = transaction.BankAccount.Id,
            };

            return View(vm);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TransactionEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = await db.Transactions.FindAsync(vm.Id);
                transaction.Description = vm.Description;
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { acctId = transaction.BankAccount.Id });
            }
            return View(vm);
        }

        // GET: Transaction/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helper methods

        public TransactionCreateViewModel SignTransactionAmount(TransactionCreateViewModel vm)
        {
            string tType = vm.TransactionType.ToString();
            string aType = vm.AccountType.ToString();
            if ((tType == "Withdrawal" && aType == "Debit") ||
                (tType == "Deposit" && aType == "Credit"))
            {
                vm.SignedAmount = vm.Amount * -1;
            }
            else
            {
                vm.SignedAmount = vm.Amount;
            }
            return vm;
        }

        public async Task<ActionResult> HandleTransactionAsync(TransactionCreateViewModel vm)
        {
            BankAccount bankAccount = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == vm.BankAccount_Id);
            bankAccount.Balance += vm.SignedAmount;
            db.Entry(bankAccount).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return null;
        }

        public async Task<TransactionHistoryViewModel> PopulateTransactionHistoryViewModelAsync(Guid acctId)
        {
            BankAccount bankAccount = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == acctId);
            List<Transaction> transactions = await db.Transactions.Where(x => x.BankAccount.Id == acctId).OrderByDescending(x => x.Date).ToListAsync();
            TransactionHistoryViewModel vm = new TransactionHistoryViewModel { BankAccount = bankAccount, Transactions = transactions };
            return vm;
        }
        #endregion
    }
}
