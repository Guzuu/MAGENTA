using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Magenta.DAL;
using Magenta.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;

namespace Magenta.Controllers
{
    [Authorize(Roles = "Admin,OfficeWorker")]
    public class ProjectsController : Controller
    {
        private readonly DefaultContext _context;

        public ProjectsController(DefaultContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var defaultContext = _context.Projects.Include(p => p.Product).Include(d => d.AddedBy);
            return View(await defaultContext.ToListAsync());
        }

        //[Route("")]
        //[Route("Projects", Order = 2)]
        //[Route("Projects/Index")]
        //[HttpGet]
        //public IActionResult Index(IFormCollection form)
        //{
        //    List<Projects> projects = new List<Projects>();
        //    var fileName = "./projects.xlsx";
        //    // For .net core, the next line requires the NuGet package, 
        //    // System.Text.Encoding.CodePages
        //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //    using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
        //    {
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {

        //            while (reader.Read()) //Each row of the file
        //            {
        //                projects.Add(new Projects
        //                {
        //                    Id = int.Parse(reader.GetValue(0).ToString()),
        //                    OrderedQuantity = int.Parse(reader.GetValue(1).ToString()),
        //                    Description = reader.GetValue(2).ToString(),
        //                    DateAdded = reader.GetDateTime(3),
        //                    DateDeadline = reader.GetDateTime(4),
        //                    Status = reader.GetValue(5).ToString(),
        //                    AttatchmentsPath = reader.GetValue(6).ToString(),
        //                    ProductId = int.Parse(reader.GetValue(7).ToString()),
        //                    AddedById = reader.GetValue(8).ToString()
        //                });
        //            }
        //        }
        //    }
        //    return View(projects);
        //}

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .Include(p => p.Product)
                .Include(d => d.AddedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["AddedById"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderedQuantity,Description,DateAdded,DateDeadline,Status,AttatchmentsPath,ProductId,AddedById")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", projects.ProductId);
            ViewData["AddedById"] = new SelectList(_context.Users, "Id", "UserName", projects.AddedById);
            return View(projects);
        }

        [Route("")]
        [Route("Projects")]
        [Route("Projects/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            Projects projects = new Projects();
            var fileName = "./projects.xlsx";
            // For .net core, the next line requires the NuGet package, 
            // System.Text.Encoding.CodePages
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each row of the file
                    {
                        projects = new Projects
                        {
                            OrderedQuantity = int.Parse(reader.GetValue(1).ToString()),
                            Description = reader.GetValue(2).ToString(),
                            DateAdded = reader.GetDateTime(3),
                            DateDeadline = reader.GetDateTime(4),
                            Status = reader.GetValue(5).ToString(),
                            AttatchmentsPath = reader.GetValue(6).ToString(),
                            ProductId = int.Parse(reader.GetValue(7).ToString()),
                            AddedById = reader.GetValue(8).ToString()
                        };

                        if (ModelState.IsValid)
                        {
                            _context.Add(projects);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", projects.ProductId);
            ViewData["AddedById"] = new SelectList(_context.Users, "Id", "UserName", projects.AddedById);
            return View(projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderedQuantity,Description,DateAdded,DateDeadline,Status,AttatchmentsPath,ProductId,AddedById")] Projects projects)
        {
            if (id != projects.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projects.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", projects.ProductId);
            ViewData["AddedById"] = new SelectList(_context.Users, "Id", "UserName", projects.AddedById);
            return View(projects);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .Include(p => p.Product)
                .Include(d => d.AddedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projects = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(projects);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
