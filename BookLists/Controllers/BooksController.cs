using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLists.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLists.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if(ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        //details
        public async Task<IActionResult> Details(int ? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == Id);
            if(book== null)
            {
                return NotFound();
            }
            return View(book);
        }

        //edit
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == Id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        // edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,Book book)
        {
            if(Id == book.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _db.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        //delete
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == Id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveBook(int id)
        {
            var book = await _db.Books.SingleOrDefaultAsync(m => m.Id == id);
            _db.Remove(book);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool Disposing)
        {
            if(Disposing)
            {
                _db.Dispose();
            }
           
        }
    }
}