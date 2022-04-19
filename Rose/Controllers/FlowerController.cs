using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rose.Abstractions;
using Rose.Data;
using Rose.Entities;
using Rose.Models.Category;
using Rose.Models.Flower;

namespace Rose.Controllers
{
    public class FlowerController : Controller
    {
        private readonly IFlowerService _flowerService;
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;

        public FlowerController(IFlowerService flowerService, ICategoryService categoryService,ApplicationDbContext context)
        {
            this._flowerService = flowerService;
            this._categoryService = categoryService;
            _context = context;
        }

        // GET: Flower
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Flowers.Include(f => f.Category).Where(f => f.Category.Name == "Flower");
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Bouquet()
        {
            var applicationDbContext = _context.Flowers.Include(f => f.Category).Where(f=>f.Category.Name=="Bouquet");
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult Statistic()
        {
            var statistic = new StatisticVM();
            statistic.userCount = _context.Users.Count();
            statistic.flowerCount= _context.Flowers.Include(f => f.Category).Where(f => f.Category.Name == "Flower").Count();
            statistic.bouqetCount=_context.Flowers.Include(f => f.Category).Where(f => f.Category.Name == "Bouquet").Count();
            statistic.orderCount = _context.Orders.Count();

            //all
           // statistic.totalPrice = _context.Orders.Sum(x=>x.Price*x.Quantity);

            //b
            statistic.totalPrice = _context.Orders.Where(x=>x.Flower.Category.Name == "Bouquet").Sum(x => x.Price * x.Quantity);


            // var applicationDbContext = _context.Flowers.Include(f => f.Category).Where(f => f.Category.Name == "Flower");
            return View(statistic);
        }



        // GET: Flower/Details/5
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

        // GET: Flower/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            //return View();

            var flower = new FlowerCreateVM();
            flower.Categories = _categoryService.GetCategories()
                .Select(c => new CategoryPairVM
                {
                    Id = c.Id,
                    Name = c.Name

                })
                .ToList();
            return View(flower);
        }

        // POST: Flower/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Description,Picture,Price,Quantity")] Flower flower)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(flower);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", flower.CategoryId);
        //    return View(flower);
        //}


        // GET: Flowers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FlowerCreateVM flower)
        {
            if (ModelState.IsValid)
            {
                var createdId = _flowerService.Create(flower.Name, flower.Price, flower.Description, flower.Quantity, flower.CategoryId, flower.Picture);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        // GET: Flower/Edit/5
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

        // POST: Flower/Edit/5
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

        // GET: Flower/Delete/5
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

        // POST: Flower/Delete/5
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
