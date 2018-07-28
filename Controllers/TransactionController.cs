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
                return RedirectToAction("Index","Home");//new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Transaction> acctTransactions = await db.Transactions.Where(x => x.BankAccount.Id == acctId).ToListAsync();
            if (acctTransactions == null)
            {
                return HttpNotFound();
            }
            return View(await db.Transactions.ToListAsync());
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
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Amount,Description,TransactionType,TransactionMethod")] Transaction transaction, Guid acctId)
        {
            if (ModelState.IsValid)
            {
                transaction.BankAccount = await db.BankAccounts.FirstOrDefaultAsync(x => x.Id == acctId);
                transaction.Id = Guid.NewGuid();
                transaction.Date = DateTimeOffset.Now;
                db.Transactions.Add(transaction);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Transaction", new { acctId });
            }

            return View(transaction);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Amount,Description,Date,TransactionType,TransactionMethod")] Transaction transaction)
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
    }
}
