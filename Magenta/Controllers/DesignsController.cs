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
    public class DesignsController : Controller
    {
        private readonly DefaultContext _context;

        public DesignsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Designs
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Designs.Include(d => d.DesignedBy).Include(d => d.Project);
            return View(await defaultContext.ToListAsync());
        }

        // GET: Designs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designs = await _context.Designs
                .Include(d => d.DesignedBy)
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designs == null)
            {
                return NotFound();
            }

            return View(designs);
        }

        // GET: Designs/Create
        public IActionResult Create()
        {
            ViewData["DesignedById"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: Designs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateDesigned,Accepted,AttatchmentsPath,ProjectId,DesignedById")] Designs designs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignedById"] = new SelectList(_context.Employees, "Id", "Id", designs.DesignedById);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", designs.ProjectId);
            return View(designs);
        }

        // GET: Designs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designs = await _context.Designs.FindAsync(id);
            if (designs == null)
            {
                return NotFound();
            }
            ViewData["DesignedById"] = new SelectList(_context.Employees, "Id", "Id", designs.DesignedById);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", designs.ProjectId);
            return View(designs);
        }

        // POST: Designs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateDesigned,Accepted,AttatchmentsPath,ProjectId,DesignedById")] Designs designs)
        {
            if (id != designs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignsExists(designs.Id))
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
            ViewData["DesignedById"] = new SelectList(_context.Employees, "Id", "Id", designs.DesignedById);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", designs.ProjectId);
            return View(designs);
        }

        // GET: Designs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designs = await _context.Designs
                .Include(d => d.DesignedBy)
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designs == null)
            {
                return NotFound();
            }

            return View(designs);
        }

        // POST: Designs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designs = await _context.Designs.FindAsync(id);
            _context.Designs.Remove(designs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignsExists(int id)
        {
            return _context.Designs.Any(e => e.Id == id);
        }
    }
}
