using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankingApplication.Models;
using BankingApplication.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BankingApplication.Controllers
{
    [Authorize]
    public class CardsController : Controller
    {
        private readonly ICardService _cardService;
        private readonly UserManager<IdentityUser> _userManager;

        public CardsController(ICardService cardService, UserManager<IdentityUser> userManager)
        {
            _cardService = cardService;
            _userManager = userManager;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
              var cards= _cardService.GetCards();
              return View(cards);
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _cardService.GetCardById(id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CardNumber,ExpirationDate,CVV,IsLocked")] Card card)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            card.User = user;
            _cardService.Create(card);
                return RedirectToAction(nameof(Index));

            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _cardService.GetCardById(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CardNumber,ExpirationDate,CVV,IsLocked")] Card card)
        {
            if (id != card.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _cardService.Update(card);
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = _cardService.GetCardById(id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var card = _cardService.GetCardById(id);
            if (card != null)
            {
                _cardService.Delete(id);
            }
           
            return RedirectToAction(nameof(Index));
        }
    }
}
