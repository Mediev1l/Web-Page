using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private ApplicationDbContext _context;



        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel formViewModel)
        {

            if (!ModelState.IsValid)
            {
                var customer = formViewModel.Customer;
                var type = _context.MembershipTypes.ToList();

                var view = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = type
                };

                return View("CustomerForm", view);
            }

            if(formViewModel.Customer.Id == 0)
              _context.Customers.Add(formViewModel.Customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == formViewModel.Customer.Id);

                customerInDb.Name = formViewModel.Customer.Name;
                customerInDb.Date = formViewModel.Customer.Date;
                customerInDb.IsSubscriberToNewsletter = formViewModel.Customer.IsSubscriberToNewsletter;
                customerInDb.MembershipType = formViewModel.Customer.MembershipType;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType);
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

    }
}