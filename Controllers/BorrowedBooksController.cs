using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class BorrowedBooksController : Controller
    {
        private readonly DataContext _context;
        [TempData]
        public string StatusMessage { get; set; }
        public BorrowedBooksController(DataContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooks
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.BorrowedBook.Include(b => b.Book).Include(b => b.LibraryCards);
            return View(await dataContext.ToListAsync());
        }

        // GET: BorrowedBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BorrowedBook == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBook
                .Include(b => b.Book)
                .Include(b => b.LibraryCards)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Create
        public IActionResult Create()
        { 
            
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
            ViewData["CardId"] = new SelectList(_context.LibraryCards, "Id", "Id");
            return View();
        }

        // POST: BorrowedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,CardId,BorrowDate,DueDate,ReturnDate")] BorrowedBook borrowedBook)
        {
            if (DateTime.Compare(borrowedBook.DueDate, borrowedBook.BorrowDate) < 0 && borrowedBook.BorrowDate != null && borrowedBook.DueDate != null)
            {
                StatusMessage = "Hạn trả không thể muộn hơn ngày mươn";
            }
            else
            {
                StatusMessage = "";
            }
            if (ModelState.IsValid && DateTime.Compare(borrowedBook.DueDate, borrowedBook.BorrowDate)>0)
            {
                _context.Add(borrowedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } 
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowedBook.BookId);
            ViewData["CardId"] = new SelectList(_context.LibraryCards, "Id", "Id", borrowedBook.CardId);
            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BorrowedBook == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBook.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowedBook.BookId);
            ViewData["CardId"] = new SelectList(_context.LibraryCards, "Id", "Id", borrowedBook.CardId);
            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,CardId,BorrowDate,DueDate,ReturnDate")] BorrowedBook borrowedBook)
        {
            if (id != borrowedBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedBookExists(borrowedBook.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", borrowedBook.BookId);
            ViewData["CardId"] = new SelectList(_context.LibraryCards, "Id", "Id", borrowedBook.CardId);
            return View(borrowedBook);
        }

        // GET: BorrowedBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BorrowedBook == null)
            {
                return NotFound();
            }

            var borrowedBook = await _context.BorrowedBook
                .Include(b => b.Book)
                .Include(b => b.LibraryCards)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            return View(borrowedBook);
        }

        // POST: BorrowedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BorrowedBook == null)
            {
                return Problem("Entity set 'DataContext.BorrowedBook'  is null.");
            }
            var borrowedBook = await _context.BorrowedBook.FindAsync(id);
            if (borrowedBook != null)
            {
                _context.BorrowedBook.Remove(borrowedBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBookExists(int id)
        {
          return (_context.BorrowedBook?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
