using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;
using BankingApplication.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BankingApplication.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IBankAccountService _bankAccountService;

        public TransactionsController(ITransactionService transactionService, IBankAccountService bankAccountService)
        {
            _transactionService = transactionService;
            _bankAccountService = bankAccountService;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transactions = _transactionService.GetTransactions(userId);
            for(int i = 0; i < transactions.Count(); i++)
            {
                transactions[i].Reciver = _bankAccountService.GetBankAccountById(transactions[i].ReciverId ?? 0);
                transactions[i].Sender = _bankAccountService.GetBankAccountById(transactions[i].SenderId ?? 0);
            }
            return View(transactions);
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = _transactionService.GetTransactionById(id);
            transaction.Reciver = _bankAccountService.GetBankAccountById(transaction.ReciverId ?? 0);
            transaction.Sender = _bankAccountService.GetBankAccountById(transaction.SenderId ?? 0);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Name");
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccountsOfUser(userId), "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,SenderId,ReciverId")] Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            if (ModelState.IsValid)
            {   
                _transactionService.Create(transaction);
                _transactionService.PerformTransaction(transaction.SenderId, transaction.ReciverId, transaction.Amount);
                return RedirectToAction(nameof(Index));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.ReciverId);
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccountsOfUser(userId), "Id", "Name");
            return View(transaction);
        }


        public IActionResult Create1()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccountsThatAreService(), "Id", "Name");
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccountsOfUser(userId), "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create1([Bind("Id,Amount,SenderId,ReciverId")] Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            if (ModelState.IsValid)
            {  
                _transactionService.Create(transaction);
                _transactionService.PerformTransaction(transaction.SenderId, transaction.ReciverId, transaction.Amount);
                return RedirectToAction(nameof(Index));
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccountsThatAreService(), "Id", "Id", transaction.ReciverId);
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccountsOfUser(userId), "Id", "Name");
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.ReciverId);
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.SenderId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Date,SenderId,ReciverId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _transactionService.Update(transaction);
                }
                catch (DbUpdateConcurrencyException)
                {  
                       throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.ReciverId);
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.SenderId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = _transactionService.GetTransactionById(id);
            if (transaction != null)
            {
                _transactionService.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
