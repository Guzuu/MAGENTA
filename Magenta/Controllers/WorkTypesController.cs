using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magenta.DAL;
using Magenta.Models;
using Microsoft.AspNetCore.Authorization;

namespace Magenta.Controllers
{
    [Authorize(Roles = "Admin,OfficeWorker")]
    public class WorkTypesController : Controller
    {
        private readonly DefaultContext _context;

        public WorkTypesController(DefaultContext context)
        {
            _context = context;
        }

        // GET: WorkTypes
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.WorkTypes.Include(w => w.Machine);
            return View(await defaultContext.ToListAsync());
        }

        // GET: WorkTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTypes = await _context.WorkTypes
                .Include(w => w.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTypes == null)
            {
                return NotFound();
            }

            return View(workTypes);
        }

        // GET: WorkTypes/Create
        public IActionResult Create()
        {
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name");
            return View();
        }

        // POST: WorkTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MachineId")] WorkTypes workTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", workTypes.MachineId);
            return View(workTypes);
        }

        // GET: WorkTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTypes = await _context.WorkTypes.FindAsync(id);
            if (workTypes == null)
            {
                return NotFound();
            }
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", workTypes.MachineId);
            return View(workTypes);
        }

        // POST: WorkTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MachineId")] WorkTypes workTypes)
        {
            if (id != workTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTypesExists(workTypes.Id))
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
            ViewData["MachineId"] = new SelectList(_context.Machines, "Id", "Name", workTypes.MachineId);
            return View(workTypes);
        }

        // GET: WorkTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTypes = await _context.WorkTypes
                .Include(w => w.Machine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTypes == null)
            {
                return NotFound();
            }

            return View(workTypes);
        }

        // POST: WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workTypes = await _context.WorkTypes.FindAsync(id);
            _context.WorkTypes.Remove(workTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTypesExists(int id)
        {
            return _context.WorkTypes.Any(e => e.Id == id);
        }
    }
}
