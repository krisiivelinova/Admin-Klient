using Microsoft.AspNetCore.Mvc;
using Rose.Data;
using Rose.Entities;
using Rose.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Controllers
{
    public class OrderController1 : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController1(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flower item = _context.Flowers.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            OrderCreateViewModel order = new OrderCreateViewModel()
            {
                //FlowerId = item.Id,

            };
            return View(order);
        }
        [HttpPost]
        public IActionResult Create(OrderCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Flowers.Find(bindingModel.FlowerId);
                if (item == null)
                {
                    return this.RedirectToAction("Create");
                }
                Order order = new Order
                {
                    UserId = bindingModel.UserId,
                    //FlowerId = bindingModel.FlowerId,
                    OrderDate = bindingModel.OrderDate,
                    Quantity = bindingModel.Quantity
                   
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                return this.RedirectToAction("All", "Flowers");
            }
            return View();
        }
    }
}
