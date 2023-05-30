using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryManagement.Controllers
{
    public class LibraryCardsController : Controller
    {
        private readonly DataContext _context;

        public LibraryCardsController(DataContext context)
        {
            _context = context;
        } 
        // GET: LibraryCards
        public async Task<IActionResult> Index()
        {
            var list = await _context.LibraryCards.ToListAsync();
              return _context.LibraryCards != null ? 
                          View(await _context.LibraryCards.ToListAsync()) :
                          Problem("Entity set 'DataContext.LibraryCards'  is null.");
        }

        // GET: LibraryCards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LibraryCards == null)
            {
                return NotFound();
            }

            var libraryCards = await _context.LibraryCards
                .FirstOrDefaultAsync(m => m.Id == id);
            //var listBooks = (from c in _context.BorrowedBook where  c.CardId == id select c).ToList();

            //var e = _context.Entry(libraryCards);
            //e.Collection(p => p.BorrowedBooks).Load();
            
            if (libraryCards == null)
            {
                return NotFound();
            }

            return View(libraryCards);
        }

        // GET: LibraryCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassId,Name,Address,DateOfBirth,Major")] LibraryCards libraryCards)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryCards);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
                return RedirectToAction(nameof(Index)); 
        }

        // GET: LibraryCards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LibraryCards == null)
            {
                return NotFound();
            }

            var libraryCards = await _context.LibraryCards.FindAsync(id);
            if (libraryCards == null)
            {
                return NotFound();
            }
            return View(libraryCards);
        }

        // POST: LibraryCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ClassId,Name,Address,DateOfBirth,Major")] LibraryCards libraryCards)
        {
            if (id != libraryCards.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryCards);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryCardsExists(libraryCards.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libraryCards);
        }

        // GET: LibraryCards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LibraryCards == null)
            {
                return NotFound();
            }

            var libraryCards = await _context.LibraryCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryCards == null)
            {
                return NotFound();
            }

            return View(libraryCards);
        }

        // POST: LibraryCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LibraryCards == null)
            {
                return Problem("Entity set 'DataContext.LibraryCards'  is null.");
            }
            var libraryCards = await _context.LibraryCards.FindAsync(id);
            if (libraryCards != null)
            {
                _context.LibraryCards.Remove(libraryCards);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryCardsExists(string id)
        {
          return (_context.LibraryCards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
