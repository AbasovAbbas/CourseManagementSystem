using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseManagement.Data;
using CourseManagement.Models;
using Microsoft.AspNetCore.Authorization;
using log4net;

namespace CourseManagement.Controllers
{ 
    //[Authorize(Roles = "Admin,Teacher")]
    public class TimeTablesController : Controller
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(TimeTablesController));
        private readonly ApplicationDbContext _context;
        public TimeTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeTables
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TimeTables.Include(t => t.Group).Include(t => t.Teacher).Include(t => t.Subject);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> IndexForStudent(int id)
        {
            var applicationDbContext = _context.TimeTables.Include(t => t.Group).Include(t => t.Teacher).Include(t => t.Subject).Where(t => t.GroupId == id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<ActionResult> IndexForTeacher(string id)
        {
            //ViewData["Id"] = id;
            var result = _context.TimeTables.Include(t => t.Group).Include(t => t.Teacher).Include(t => t.Subject).Where(x => x.TeacherId == id);
            return View(result);
        }
        // GET: TimeTables/Details/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeTable = await _context.TimeTables
                .Include(t => t.Group)
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeTable == null)
            {
                return NotFound();
            }

            return View(timeTable);
        }

        // GET: TimeTables/Create
        [Authorize(Roles = "Admin,Teacher")]
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Title");
            ViewData["TeacherId"] = new SelectList(_context.Users.Where(x => x.EnrollmentNo == 0), "Id", "Name");
            return View();
        }

        // POST: TimeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create([Bind("Id,Time,TeacherId,SubjectId,GroupId")] TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeTable);
                await _context.SaveChangesAsync();
                _log.Info("Timetable added by admin");
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", timeTable.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Title", timeTable.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users.Where(x => x.EnrollmentNo == 0), "Id", "Name", timeTable.TeacherId);
            return View(timeTable);
        }

        /*[Authorize(Roles = "Admin,Teacher")]
        public IActionResult CreateForTeacher(string id)
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.TeacherSubject.Where(s => s.TeacherId == id).Select(x => x.Subject), "Id", "Title");
            return View();
        }

        // POST: TimeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CreateForTeacher(string id, [Bind("Id,Time,TeacherId,StudentId,SubjectId")] TimeTable timeTable)
        {
            if (ModelState.IsValid)
            {
                timeTable.TeacherId = id;
                _context.Add(timeTable);
                _log.Info("TimeTable added by teacher");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", timeTable.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.TeacherSubject.Where(s => s.TeacherId == id).Select(x => x.Subject), "Id", "Title", timeTable.SubjectId);

            return View(timeTable);
        }*/

        // GET: TimeTables/Edit/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeTable = await _context.TimeTables.FindAsync(id);
            if (timeTable == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", timeTable.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Title", timeTable.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users.Where(x => x.EnrollmentNo == 0), "Id", "Name", timeTable.TeacherId);
            return View(timeTable);
        }

        // POST: TimeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time,TeacherId,GroupId,SubjectId")] TimeTable timeTable)
        {
            if (id != timeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeTableExists(timeTable.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", timeTable.GroupId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Title", timeTable.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users.Where(x => x.EnrollmentNo==0), "Id", "Name", timeTable.TeacherId);
            return View(timeTable);
        }

        // GET: TimeTables/Delete/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeTable = await _context.TimeTables
                .Include(t => t.Group)
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeTable == null)
            {
                return NotFound();
            }

            return View(timeTable);
        }

        // POST: TimeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Teacher")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeTable = await _context.TimeTables.FindAsync(id);
            _context.TimeTables.Remove(timeTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeTableExists(int id)
        {
            return _context.TimeTables.Any(e => e.Id == id);
        }
    }
}
