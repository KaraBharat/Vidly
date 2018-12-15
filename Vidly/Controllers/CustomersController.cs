using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customerViewModel = new CustomerViewModels();
            var customers = new List<Customer>();

            customers.Add(new Customer { Id = 1, Name = "Bharat Kara" });
            customers.Add(new Customer { Id = 2, Name = "John Smith" });

            customerViewModel.Customers = customers;

            return View(customerViewModel);
        }

        [Route("customers/details/{Id}")]
        public ActionResult Detail(int Id)
        {

            var customerViewModel = new CustomerViewModels();
            var customers = new List<Customer>();

            customers.Add(new Customer { Id = 1, Name = "Bharat Kara" });
            customers.Add(new Customer { Id = 2, Name = "John Smith" });


            var customer = customers.Where(s => s.Id == Id).FirstOrDefault();

            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}