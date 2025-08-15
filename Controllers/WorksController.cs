using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScribeTracker.Data;
using ScribeTracker.Models;
using ScribeTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScribeTracker.Controllers
{
    public class WorksController : Controller
    {
        private readonly ScribeContext _context;

        public WorksController(ScribeContext context)
        {
            _context = context;
        }

        // GET: Works
        public async Task<IActionResult> Index()
        {
            var works = await _context.Works
                .Include(w => w.PenName) // This is key
                .ToListAsync();

            return View(works);
        }

        // GET: Works/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var work = await _context.Works
                .Include(w => w.PenName)
                .Include(w => w.Submissions)
                    .ThenInclude(s => s.Market)
                .FirstOrDefaultAsync(m => m.WorkId == id);

            if (work == null) return NotFound();

            return View(work);
        }

        // GET: Works/Create
        public IActionResult Create()
        {
            var penNames = _context.PenNames
         .Select(p => new SelectListItem
         {
             Value = p.PenNameId.ToString(),
             Text = p.Name
         }).ToList();

            penNames.Insert(0, new SelectListItem { Value = "", Text = "-- Select Pen Name --" });

            var workTypes = Enum.GetValues(typeof(WorkType))
                .Cast<WorkType>()
                .Select(wt => new SelectListItem
                {
                    Value = ((int)wt).ToString(),
                    Text = wt.ToString()
                }).ToList();

            workTypes.Insert(0, new SelectListItem { Value = "", Text = "-- Select Work Type --" });

            var viewModel = new WorkPenNameViewModel
            {
                PenNames = penNames,
                WorkTypes = workTypes
            };

            return View(viewModel);

        }



        // POST: Works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WorkPenNameViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PenNames = _context.PenNames
                    .Select(p => new SelectListItem
                    {
                        Value = p.PenNameId.ToString(),
                        Text = p.Name
                    }).ToList();

                return View(vm);
            }

            var work = new Work
            {
                Title = vm.Title,
                Type = vm.Type,
                PenNameId = vm.PenNameId
            };

            _context.Works.Add(work);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Works/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }

            var viewModel = new WorkPenNameViewModel
            {
                Title = work.Title,
                Type = work.Type,
                PenNameId = work.PenNameId,
                PenNames = await _context.PenNames
                    .Select(p => new SelectListItem
                    {
                        Value = p.PenNameId.ToString(),
                        Text = p.PenNameId.ToString() // Or use p.DisplayName if available
                    }).ToListAsync(),
                WorkTypes = Enum.GetValues(typeof(WorkType))
                    .Cast<WorkType>()
                    .Select(wt => new SelectListItem
                    {
                        Value = ((int)wt).ToString(),
                        Text = wt.ToString()
                    }).ToList()
            };

            return View(viewModel);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkPenNameViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns before returning view
                model.PenNames = await _context.PenNames
                    .Select(p => new SelectListItem
                    {
                        Value = p.PenNameId.ToString(),
                        Text = p.PenNameId.ToString() // Or p.DisplayName
                    }).ToListAsync();

                model.WorkTypes = Enum.GetValues(typeof(WorkType))
                    .Cast<WorkType>()
                    .Select(wt => new SelectListItem
                    {
                        Value = ((int)wt).ToString(),
                        Text = wt.ToString()
                    }).ToList();

                return View(model);
            }

            var workToUpdate = await _context.Works.FindAsync(id);
            if (workToUpdate == null)
            {
                return NotFound();
            }

            // Map ViewModel to entity
            workToUpdate.Title = model.Title;
            workToUpdate.Type = model.Type;
            workToUpdate.PenNameId = model.PenNameId;

            try
            {
                _context.Update(workToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Works/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var work = await _context.Works
                .Include(w => w.PenName)
                .FirstOrDefaultAsync(m => m.WorkId == id);
            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work != null)
            {
                _context.Works.Remove(work);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExists(int id)
        {
            return _context.Works.Any(e => e.WorkId == id);
        }
    }
}
