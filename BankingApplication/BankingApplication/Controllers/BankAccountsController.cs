using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;
using Microsoft.AspNetCore.Identity;
using BankingApplication.Services.Interfaces;
using System.Security.Claims;

namespace BankingApplication.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountService _bankAccountService;

        private readonly UserManager<IdentityUser> _userManager;

        public BankAccountsController(IBankAccountService bankAccountService, UserManager<IdentityUser> userManager)
        {
            _bankAccountService = bankAccountService;
            _userManager = userManager;
        }

        // GET: BankAccounts
        public async Task<IActionResult> Index()
        {
            var bankAccounts= _bankAccountService.GetBankAccounts();
            return View(bankAccounts);
        }

        // GET: BankAccounts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = _bankAccountService.GetBankAccountById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IBAN,SWIFT,Balance,Currency")] BankAccount bankAccount)
        {           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            bankAccount.User = user;

            //if (ModelState.IsValid)
            //{
                _bankAccountService.Create(bankAccount);
                return RedirectToAction(nameof(Index));
            //}
            return View(bankAccount);
        }

        // GET: BankAccounts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = _bankAccountService.GetBankAccountById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IBAN,SWIFT,Balance,Currency")] BankAccount bankAccount)
        {
            if (id != bankAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bankAccountService.Create(bankAccount);
                }
                catch (DbUpdateConcurrencyException)
                { 
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankAccount = _bankAccountService.GetBankAccountById(id);
            if (bankAccount == null)
            {
                return NotFound();
            }

            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bankAccount = _bankAccountService.GetBankAccountById(id);
            if (bankAccount != null)
            {
                _bankAccountService.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
