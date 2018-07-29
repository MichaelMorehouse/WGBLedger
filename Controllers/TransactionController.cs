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
    public class TransactionController : Controller, ITransaction
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transaction
        public async Task<ActionResult> Index(Guid? acctId)
        {
            if (acctId == null)
            {
                return RedirectToAction("Index","Home");//new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(acctId);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            TransactionCreateViewModel transaction = new TransactionCreateViewModel();
            transaction.BankAccount_Id = (Guid)acctId;
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
                Transaction transaction = new Transaction
                {
                    Id = Guid.NewGuid(),
                    Date = DateTimeOffset.Now,
                    BankAccount = bankAccount,
                    PreviousBalance = bankAccount.Balance,
                    Amount = vm.Amount,
                    Description = vm.Description,
                    TransactionMethod = vm.TransactionMethod,
                    TransactionType = vm.TransactionType,
                };
                await HandleTransaction(vm);
                transaction.NewBalance = bankAccount.Balance;
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Transaction", new { acctId = vm.BankAccount_Id });
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
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Amount,Description,Date,TransactionType,TransactionMethod")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(transaction);
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

        public async Task<double> HandleTransaction(TransactionCreateViewModel vm)
        {
            BankAccount bankAccount = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == vm.BankAccount_Id);
            if (vm.TransactionType.ToString() == "Withdrawal") bankAccount.Balance -= vm.Amount;
            else bankAccount.Balance += vm.Amount;
            db.Entry(bankAccount).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return bankAccount.Balance;
        }

        public async Task<TransactionHistoryViewModel> PopulateTransactionHistoryViewModelAsync(Guid acctId)
        {
            BankAccount bankAccount = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == acctId);
            List<Transaction> transactions = await db.Transactions.Where(x => x.BankAccount.Id == acctId).OrderByDescending(x => x.Date).ToListAsync();
            TransactionHistoryViewModel vm = new TransactionHistoryViewModel { BankAccount = bankAccount, Transactions = transactions };
            return vm;
        }
    }
}
