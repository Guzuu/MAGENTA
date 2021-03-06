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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Magenta.Controllers
{
    [Authorize(Roles = "Admin,GraphicDesigner")]
    public class DesignsController : Controller
    {
        private readonly DefaultContext _context; 
        private IWebHostEnvironment _hostingEnvironment;

        public DesignsController(DefaultContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: Designs
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Designs.Include(d => d.Project).Include(d => d.DesignedBy);
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
                .Include(d => d.Project)
                .Include(d => d.DesignedBy)
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            ViewData["DesignedById"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Designs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateDesigned,Accepted,AttatchmentsPath,ProjectId,DesignedById")] Designs designs, IList<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                foreach (IFormFile file in postedFiles)
                {
                    if (file.Length > 0)
                    {
                        string dirPath = Path.Combine(uploads, "designs\\" + designs.ProjectId);
                        if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                        string filePath = Path.Combine(dirPath, file.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            designs.AttatchmentsPath = dirPath;
                        }
                    }
                }
                _context.Add(designs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", designs.ProjectId);
            ViewData["DesignedById"] = new SelectList(_context.Users, "Id", "UserName", designs.DesignedById);
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", designs.ProjectId);
            ViewData["DesignedById"] = new SelectList(_context.Users, "Id", "UserName", designs.DesignedById);
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", designs.ProjectId);
            ViewData["DesignedById"] = new SelectList(_context.Users, "Id", "UserName", designs.DesignedById);
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
                .Include(d => d.Project)
                .Include(d => d.DesignedBy)
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
