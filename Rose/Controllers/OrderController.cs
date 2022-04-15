using Microsoft.AspNetCore.Mvc;
using Rose.Data;
using Rose.Entities;
using Rose.Models.Order;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rose.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
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
                FlowerId = item.Id,

            };
            return View(order);
        }
        [HttpPost]
        public IActionResult Create(OrderCreateViewModel bindingModel)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this._context.Users.SingleOrDefault(u => u.Id == userId);
            var ev = this._context.Flowers.SingleOrDefault(e => e.Id == bindingModel.FlowerId);
            if (ModelState.IsValid)
            {
                var item = _context.Flowers.Find(bindingModel.FlowerId);
                if (item == null)
                {
                    return this.RedirectToAction("Create");
                }
                Order order = new Order
                {
                    UserId = userId,
                    FlowerId = bindingModel.FlowerId,
                   // OrderDate = bindingModel.OrderDate,
                    Quantity = bindingModel.Quantity,
                    Price=ev.Price,

                   
                };

                _context.Orders.Add(order);
                _context.SaveChanges();
                return this.RedirectToAction("AllOrders", "Order");
            }
            return View();
        }
        //public int Id { get; set; }
        //[Required]
        //public int FlowerId { get; set; }
        //[Required]
        //[Range(1, int.MaxValue)]
        //[Display(Name = "Quantity")]
        //public int Quantity { get; set; }

        //public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }

        //[MinLength(10)]
        //[MaxLength(50)]
        //public DateTime OrderDate { get; set; }
        public IActionResult AllOrders()
        {
            //string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            List<OrderListingViewModel> orders = _context
                 .Orders
                 .Select(x => new OrderListingViewModel
                 {
                     Id = x.Id,
                     FlowerId = x.FlowerId,
                     Quantity = x.Quantity,
                     UserId=x.UserId,
                     UserName = x.User.UserName,
                     OrderDate = x.OrderDate.ToString("dd-mm-yyyy hh:mm", CultureInfo.InvariantCulture),
               FlowerName = x.Flower.Name

                 }).ToList();

            return View(orders);
        }
    }
}
