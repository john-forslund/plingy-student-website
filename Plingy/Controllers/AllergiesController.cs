using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Plingy.Models;

namespace Plingy.Controllers
{
    public class AllergiesController : Controller
    {
        private readonly PlingyDbContext _context;

        public AllergiesController(PlingyDbContext context)
        {
            _context = context;
        }

        // GET: Allergies
        public async Task<IActionResult> Index()
        {
            var plingyDbContext = _context.Allergies.Include(a => a.Student);
            return View(await plingyDbContext.ToListAsync());
        }

        // GET: Allergies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergies = await _context.Allergies
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AllergiesID == id);
            if (allergies == null)
            {
                return NotFound();
            }

            return View(allergies);
        }

        // GET: Allergies/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Name");
            return View();
        }

        // POST: Allergies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AllergiesID,StudentID,Allergy")] Allergies allergies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allergies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Name", allergies.StudentID);
            return View(allergies);
        }

        // GET: Allergies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergies = await _context.Allergies.FindAsync(id);
            if (allergies == null)
            {
                return NotFound();
            }
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Name", allergies.StudentID);
            return View(allergies);
        }

        // POST: Allergies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AllergiesID,StudentID,Allergy")] Allergies allergies)
        {
            if (id != allergies.AllergiesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allergies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllergiesExists(allergies.AllergiesID))
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
            ViewData["StudentID"] = new SelectList(_context.Student, "StudentID", "Name", allergies.StudentID);
            return View(allergies);
        }

        // GET: Allergies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allergies = await _context.Allergies
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.AllergiesID == id);
            if (allergies == null)
            {
                return NotFound();
            }

            return View(allergies);
        }

        // POST: Allergies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allergies = await _context.Allergies.FindAsync(id);
            _context.Allergies.Remove(allergies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllergiesExists(int id)
        {
            return _context.Allergies.Any(e => e.AllergiesID == id);
        }
    }
}
