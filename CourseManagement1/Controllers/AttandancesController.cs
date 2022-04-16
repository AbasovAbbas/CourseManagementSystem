using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseManagement.Data;
using CourseManagement.Models;
using CourseManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CourseManagement.Controllers
{
    [Authorize(Roles = "Teacher,Admin")]
    public class AttandancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttandancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attandances
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Attandance.Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attandances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attandance = await _context.Attandance
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attandance == null)
            {
                return NotFound();
            }

            return View(attandance);
        }

        // GET: Attandances/Create
        public IActionResult Create(int id, DateTime time)
        {
            ViewBag.Time = time;
            /*var vmodel = new List<StudentAttandance>()
            {
                new StudentAttandance{Students = _context.Users.Where(x => x.EnrollmentNo > 0 && x.GroupId == id).ToList()}
            };*/
            ViewData["StudentId"] = _context.Users.Where(x => x.EnrollmentNo > 0 && x.GroupId == id).ToList();
            return View();
        }

        // POST: Attandances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, List<Attandance> attandance)
        {
            for (int i = 0; i < attandance.Count; i++)
            {
                if (ModelState.IsValid)
                {
                    _context.Attandance.Add(attandance[i]);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
            ViewData["StudentId"] = _context.Users.Where(x => x.EnrollmentNo > 0 && x.GroupId == id).ToList();
            return View(attandance);
        }

        // GET: Attandances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attandance = await _context.Attandance.FindAsync(id);
            if (attandance == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = _context.Users.Where(x => x.EnrollmentNo > 0 && x.GroupId == id).ToList();
            return View(attandance);
        }

        // POST: Attandances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time,AttandanceType,StudentId")] Attandance attandance)
        {
            if (id != attandance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attandance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttandanceExists(attandance.Id))
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
            ViewData["StudentId"] = _context.Users.Where(x => x.EnrollmentNo > 0 && x.GroupId == id).ToList(); 
            return View(attandance);
        }

        // GET: Attandances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attandance = await _context.Attandance
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attandance == null)
            {
                return NotFound();
            }

            return View(attandance);
        }

        // POST: Attandances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attandance = await _context.Attandance.FindAsync(id);
            _context.Attandance.Remove(attandance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttandanceExists(int id)
        {
            return _context.Attandance.Any(e => e.Id == id);
        }
    }
}
