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
using Microsoft.AspNet.Identity;

namespace WGBLedger.Controllers
{
    public class BankAccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccount
        public async Task<ActionResult> Index()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            if (userId == null)
            {
                return View("Home");
            }
            return View(await db.BankAccounts.Where(x => x.User.Id == userId).ToListAsync());
        }

        // GET: BankAccount/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,AccountType")] BankAccount bankAccount)
        {
            string userId = HttpContext.User.Identity.GetUserId();
            if (userId == null)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                bankAccount.User = await db.Users.SingleOrDefaultAsync(x => x.Id.ToString() == userId);
                bankAccount.Id = Guid.NewGuid();
                bankAccount.DateCreated = DateTimeOffset.Now;
                db.BankAccounts.Add(bankAccount);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bankAccount);
        }

        // GET: BankAccount/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }

            BankAccountEditViewModel vm = new BankAccountEditViewModel
            {
                Id = bankAccount.Id,
                Name = bankAccount.Name,
                AccountType = bankAccount.AccountType,
                DateCreated = bankAccount.DateCreated
            };

            return View(vm);
        }

        // POST: BankAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BankAccountEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                BankAccount bankAccount = await db.BankAccounts.FindAsync(vm.Id);
                bankAccount.Name = vm.Name;
                db.Entry(bankAccount).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: BankAccount/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            List<Transaction> transactions = await db.Transactions.Where(x => x.BankAccount.Id == id).ToListAsync();
            db.Transactions.RemoveRange(transactions);
            BankAccount bankAccount = await db.BankAccounts.FindAsync(id);
            db.BankAccounts.Remove(bankAccount);
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
