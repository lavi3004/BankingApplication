﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;
using BankingApplication.Services.Interfaces;

namespace BankingApplication.Controllers
{
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
            var transactions = _transactionService.GetTransactions();
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
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id");
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Date,SenderId,ReciverId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _transactionService.Create(transaction);
                _transactionService.PerformTransaction(transaction.SenderId, transaction.ReciverId, transaction.Amount);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReciverId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.ReciverId);
            ViewData["SenderId"] = new SelectList(_bankAccountService.GetBankAccounts(), "Id", "Id", transaction.SenderId);
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