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
    public class SubmissionsController : Controller
    {
        private readonly ScribeContext _context;

        public SubmissionsController(ScribeContext context)
        {
            _context = context;
        }

        // GET: Submissions
        public async Task<IActionResult> Index()
        {
            var scribeContext = _context.Submissions.Include(s => s.Market).Include(s => s.Work);
            return View(await scribeContext.ToListAsync());
        }

        // GET: Submissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var submission = await _context.Submissions
                .Include(s => s.Work)
                    .ThenInclude(w => w.PenName)
                .Include(s => s.Market)
                .FirstOrDefaultAsync(m => m.SubmissionId == id);

            if (submission == null) return NotFound();

            return View(submission);
        }

        // GET: Submissions/Create
        public IActionResult Create()
        {
            ViewData["MarketId"] = new SelectList(_context.Markets, "MarketId", "MarketId");
            ViewData["WorkId"] = new SelectList(_context.Works, "WorkId", "WorkId");
            return View();
        }

        // POST: Submissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubmissionId,WorkId,MarketId,SubmissionDate,Status,Payment,Notes")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(submission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarketId"] = new SelectList(_context.Markets, "MarketId", "MarketId", submission.MarketId);
            ViewData["WorkId"] = new SelectList(_context.Works, "WorkId", "WorkId", submission.WorkId);
            return View(submission);
        }

        // GET: Submissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }
            ViewData["MarketId"] = new SelectList(_context.Markets, "MarketId", "MarketId", submission.MarketId);
            ViewData["WorkId"] = new SelectList(_context.Works, "WorkId", "WorkId", submission.WorkId);
            return View(submission);
        }

        // POST: Submissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubmissionId,WorkId,MarketId,SubmissionDate,Status,Payment,Notes")] Submission submission)
        {
            if (id != submission.SubmissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(submission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.SubmissionId))
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
            ViewData["MarketId"] = new SelectList(_context.Markets, "MarketId", "MarketId", submission.MarketId);
            ViewData["WorkId"] = new SelectList(_context.Works, "WorkId", "WorkId", submission.WorkId);
            return View(submission);
        }

        // GET: Submissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var submission = await _context.Submissions
                .Include(s => s.Market)
                .Include(s => s.Work)
                .FirstOrDefaultAsync(m => m.SubmissionId == id);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission != null)
            {
                _context.Submissions.Remove(submission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(int id)
        {
            return _context.Submissions.Any(e => e.SubmissionId == id);
        }
    }
}
