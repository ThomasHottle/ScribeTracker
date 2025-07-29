using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScribeTracker.Data;
using ScribeTracker.Models;

namespace ScribeTracker.Controllers
{
    public class PenNamesController : Controller
    {
        private readonly ScribeContext _context;

        public PenNamesController(ScribeContext context)
        {
            _context = context;
        }

        // GET: PenNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.PenNames.ToListAsync());
        }

        // GET: PenNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penName = await _context.PenNames
                .FirstOrDefaultAsync(m => m.PenNameId == id);
            if (penName == null)
            {
                return NotFound();
            }

            return View(penName);
        }

        // GET: PenNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PenNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PenNameId,Name")] PenName penName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(penName);
        }

        // GET: PenNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penName = await _context.PenNames.FindAsync(id);
            if (penName == null)
            {
                return NotFound();
            }
            return View(penName);
        }

        // POST: PenNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PenNameId,Name")] PenName penName)
        {
            if (id != penName.PenNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenNameExists(penName.PenNameId))
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
            return View(penName);
        }

        // GET: PenNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penName = await _context.PenNames
                .FirstOrDefaultAsync(m => m.PenNameId == id);
            if (penName == null)
            {
                return NotFound();
            }

            return View(penName);
        }

        // POST: PenNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var penName = await _context.PenNames.FindAsync(id);
            if (penName != null)
            {
                _context.PenNames.Remove(penName);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenNameExists(int id)
        {
            return _context.PenNames.Any(e => e.PenNameId == id);
        }
    }
}
