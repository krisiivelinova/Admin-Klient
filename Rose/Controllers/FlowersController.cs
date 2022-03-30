using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rose.Abstractions;
using Rose.Data;
using Rose.Entities;
using Rose.Models.Flower;

namespace Rose.Controllers
{
    [Authorize]
    public class FlowersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFlowerService _flowerService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnviroment _hostEnviroment;

        public FlowersController(IFlowerService flowerService, ICategoryService categoryService, ApplicationDbContext _context, IWebHostEnviroment hostEnviroment)
        {
            this._flowerService = flowerService;
            this._categoryService = categoryService;
            this._hostEnviroment = hostEnviroment;
            this._context = _context;
        }
             
    

        [AllowAnonymous]
        // GET: Flowers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Flowers.Include(f => f.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Flowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // GET: Flowers/Create

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }


        // POST: Flowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Description,Picture,Price,Quantity")] Flower flower)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FlowerCreateVM input)
    {
        var imagePath = $"{this._hostEnviroment.WebRootPath}";
        //if (ModelState.IsValid)
        //{
        //    _context.Add(flower);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", flower.CategoryId);
        //return View(flower);
        if (!ModelState.IsValid)
        {
            input.Categories = _categoryService.GetCategories()
                .Select(char => new CategoryPairVM()
                {
                    Id = c.Id,
                    Name = char.Name
                })
                .ToList();
            return View(input);
        }
        await this._flowerService.Create(input, imagePath);
        return RedirectToAction(nameof(Index));
    }

        // GET: Flowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", flower.CategoryId);
            return View(flower);
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId,Description,Picture,Price,Quantity")] Flower flower)
        {
            if (id != flower.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlowerExists(flower.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", flower.CategoryId);
            return View(flower);
        }

        // GET: Flowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _context.Flowers
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flower = await _context.Flowers.FindAsync(id);
            _context.Flowers.Remove(flower);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlowerExists(int id)
        {
            return _context.Flowers.Any(e => e.Id == id);
        }
    }
}
