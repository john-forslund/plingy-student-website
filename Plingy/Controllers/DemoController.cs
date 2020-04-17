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
    public class DemoController : Controller
    {
        private readonly PlingyDbContext _context;

        public DemoController(PlingyDbContext context)
        {
            _context = context;
        }

        // GET: Demo
        public async Task<IActionResult> Index()
        {
            var viewModel = new StudentAllergiesViewModel();
            viewModel.Students = await _context.Student
                .Include(a => a.StudentsAllergies)
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Demo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var student = await _context.Student
                .Include(a => a.StudentsAllergies)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Demo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Demo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,Name,JoinDate,ActiveStudent,Address")] Student student, string[] selectedAllergies)
        {

            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();

                if (selectedAllergies.Length != 0) {
                    var allergies = new List<Allergies>();
                    //Console.WriteLine("selectedAllergies.Length:" + selectedAllergies.Length);
                    //Console.WriteLine("selectedAllergies:");
                    //Array.ForEach(selectedAllergies, Console.WriteLine);
                    for (var i = 0; i < selectedAllergies.Length; i++) {
                        if (String.IsNullOrEmpty(selectedAllergies[i]) == false) {
                            //Console.WriteLine(selectedAllergies[i] + " is NOT empty!");
                            allergies.Add(new Allergies { StudentID = student.StudentID,
                                                        Allergy = selectedAllergies[i]});
                        } else {
                            //Console.WriteLine(selectedAllergies[i] + " is empty!");
                        }
                    }
                    _context.AddRange(allergies);
                    await _context.SaveChangesAsync();
                } else {
                    Console.WriteLine("selectedAllergies empty!");
                }
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Demo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(a => a.StudentsAllergies)
                .FirstOrDefaultAsync(m => m.StudentID == id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Demo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentID,Name,JoinDate,ActiveStudent,Address")] Student student, string[] selectedAllergies)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();

                    var test = await _context.Student
                        .Include(a => a.StudentsAllergies)
                        .FirstOrDefaultAsync(m => m.StudentID == id);
                    
                    var currentAllergies = test.StudentsAllergies;
                    
                    Console.WriteLine("currentAllergies:");
                    Console.WriteLine(currentAllergies);
                    Array.ForEach(selectedAllergies, Console.WriteLine);

                    if (selectedAllergies.Length != 0) {
                        var allergies = new List<Allergies>();
                        //Console.WriteLine("selectedAllergies.Length:" + selectedAllergies.Length);
                        //Console.WriteLine("selectedAllergies:");
                        //Array.ForEach(selectedAllergies, Console.WriteLine);
                        for (var i = 0; i < selectedAllergies.Length; i++) {
                            if (String.IsNullOrEmpty(selectedAllergies[i]) == false) {
                                //Console.WriteLine(selectedAllergies[i] + " is NOT empty!");

                                allergies.Add(new Allergies { StudentID = student.StudentID,
                                                            Allergy = selectedAllergies[i]});
                            } else {
                                //Console.WriteLine(selectedAllergies[i] + " is empty!");
                            }
                        }
                        _context.Allergies.RemoveRange(currentAllergies);
                        _context.AddRange(allergies);
                        await _context.SaveChangesAsync();
                    } else {
                        Console.WriteLine("selectedAllergies empty!");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
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
            return View(student);
        }

        // GET: Demo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(a => a.StudentsAllergies)
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }
    }
}
