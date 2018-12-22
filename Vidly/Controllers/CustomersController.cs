using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customerViewModel = new CustomerViewModels();

            customerViewModel.Customers = _dbContext.Customer.Include(c => c.MembershipType).ToList();

            return View(customerViewModel);
        }

        public ActionResult New()
        {
            var newCustomer = new CustomerFormViewModels()
            {
                Customer = new Customer(),
                MembershipType = _dbContext.MembershipType.ToList()
            };
            return View("CustomerForm", newCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerForm = new CustomerFormViewModels()
                {
                    Customer = customer,
                    MembershipType = _dbContext.MembershipType.ToList()
                };


                return View("CustomerForm", customerForm);
            }


            if (customer.Id == 0)
            {
                _dbContext.Customer.Add(customer);
            }
            else
            {
                var customerInDb = _dbContext.Customer.Where(s => s.Id == customer.Id).Include(c => c.MembershipType).First();
                customerInDb.Name = customer.Name;
                customerInDb.DateOfBirth = customer.DateOfBirth;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        [Route("customers/details/{Id}")]
        public ActionResult Detail(int Id)
        {
            var customer = _dbContext.Customer.Where(s => s.Id == Id).Include(c => c.MembershipType).FirstOrDefault();

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [Route("customers/edit/{Id}")]
        public ActionResult Edit(int Id)
        {
            var customer = _dbContext.Customer.Where(s => s.Id == Id).Include(c => c.MembershipType).FirstOrDefault();

            if (customer == null)
                return HttpNotFound();

            var customerForm = new CustomerFormViewModels()
            {
                Customer = customer,
                MembershipType = _dbContext.MembershipType.ToList()
            };


            return View("CustomerForm", customerForm);
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}