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
    public class MachinesController : Controller
    {
        private readonly DefaultContext _context;

        public MachinesController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Machines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Machines.ToListAsync());
        }

        // GET: Machines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machines = await _context.Machines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machines == null)
            {
                return NotFound();
            }

            return View(machines);
        }

        // GET: Machines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Machines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RequiresCourse")] Machines machines)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machines);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machines);
        }

        // GET: Machines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machines = await _context.Machines.FindAsync(id);
            if (machines == null)
            {
                return NotFound();
            }
            return View(machines);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RequiresCourse")] Machines machines)
        {
            if (id != machines.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machines);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachinesExists(machines.Id))
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
            return View(machines);
        }

        // GET: Machines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machines = await _context.Machines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machines == null)
            {
                return NotFound();
            }

            return View(machines);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machines = await _context.Machines.FindAsync(id);
            _context.Machines.Remove(machines);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachinesExists(int id)
        {
            return _context.Machines.Any(e => e.Id == id);
        }
    }
}
