using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rose.Data;
using Rose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rose.Controllers
{
    public class ClientController1 : Controller
    {
        private readonly ApplicationDbContext context;

        public ClientController1(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: ClientController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClientController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: ClientController
        public ActionResult AllClients()
        {
            List<ClientBindingAllViewModel> users = context.Users.Select(
                clients => new ClientBindingAllViewModel
                {
                    Id = clients.Id,
                    UserName = clients.UserName,
                    FirstName = clients.FirstName,
                    LastName = clients.LastName,
                    Email = clients.Email,
                    PhoneNumber = clients.PhoneNumber
                }).ToList();
            return View(users);
        }
    }
}
