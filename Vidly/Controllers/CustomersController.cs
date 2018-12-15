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

        [Route("customers/details/{Id}")]
        public ActionResult Detail(int Id)
        {
            var customer = _dbContext.Customer.Where(s => s.Id == Id).Include(c => c.MembershipType).FirstOrDefault();

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}