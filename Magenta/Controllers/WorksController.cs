using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magenta.DAL;
using Magenta.Models;

namespace Magenta.Controllers
{
    public class WorksController : Controller
    {
        private readonly DefaultContext _context;

        public WorksController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Works
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Works.Include(w => w.Department).Include(w => w.ProcessedBy).Include(w => w.Project).Include(w => w.WorkType);
            return View(await defaultContext.ToListAsync());
        }

        // GET: Works/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var works = await _context.Works
                .Include(w => w.Department)
                .Include(w => w.ProcessedBy)
                .Include(w => w.Project)
                .Include(w => w.WorkType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (works == null)
            {
                return NotFound();
            }

            return View(works);
        }

        // GET: Works/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            ViewData["ProcessedById"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Id");
            return View();
        }

        // POST: Works/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AmountProcessed,AdditionalInfo,DateProcessed,ProjectId,WorkTypeId,DepartmentId,ProcessedById")] Works works)
        {
            if (ModelState.IsValid)
            {
                _context.Add(works);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", works.DepartmentId);
            ViewData["ProcessedById"] = new SelectList(_context.Employees, "Id", "Id", works.ProcessedById);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", works.ProjectId);
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Id", works.WorkTypeId);
            return View(works);
        }

        // GET: Works/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var works = await _context.Works.FindAsync(id);
            if (works == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", works.DepartmentId);
            ViewData["ProcessedById"] = new SelectList(_context.Employees, "Id", "Id", works.ProcessedById);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", works.ProjectId);
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Id", works.WorkTypeId);
            return View(works);
        }

        // POST: Works/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AmountProcessed,AdditionalInfo,DateProcessed,ProjectId,WorkTypeId,DepartmentId,ProcessedById")] Works works)
        {
            if (id != works.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(works);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorksExists(works.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", works.DepartmentId);
            ViewData["ProcessedById"] = new SelectList(_context.Employees, "Id", "Id", works.ProcessedById);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", works.ProjectId);
            ViewData["WorkTypeId"] = new SelectList(_context.WorkTypes, "Id", "Id", works.WorkTypeId);
            return View(works);
        }

        // GET: Works/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var works = await _context.Works
                .Include(w => w.Department)
                .Include(w => w.ProcessedBy)
                .Include(w => w.Project)
                .Include(w => w.WorkType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (works == null)
            {
                return NotFound();
            }

            return View(works);
        }

        // POST: Works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var works = await _context.Works.FindAsync(id);
            _context.Works.Remove(works);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorksExists(int id)
        {
            return _context.Works.Any(e => e.Id == id);
        }
    }
}
