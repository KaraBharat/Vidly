using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public CustomersController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET api/customers
        public IHttpActionResult GetCustomers()
        {
            var customers = _dbContext.Customer
                .Include(s => s.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            if (customers == null || !customers.Any())
                return NotFound();

            return Ok(customers);
        }

        // GET api/customers/1
        public IHttpActionResult GetCustomersById(int Id)
        {
            var customer = _dbContext.Customer.SingleOrDefault(s => s.Id == Id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomers(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            //throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _dbContext.Customer.Add(customer);
            _dbContext.SaveChanges();

            customerDto.Id = customer.Id;

            //return customerDto;

            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
        }

        [HttpPut]
        // PUT api/customers/1
        public IHttpActionResult UpdateCustomers(int Id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _dbContext.Customer.SingleOrDefault(s => s.Id == Id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            _dbContext.SaveChanges();

            return Ok(customerDto);
        }

        [HttpDelete]
        // DELETE api/customers/1
        public IHttpActionResult DeleteCustomers(int Id)
        {
            var customerInDb = _dbContext.Customer.SingleOrDefault(s => s.Id == Id);

            if (customerInDb == null)
                return NotFound();

            _dbContext.Customer.Remove(customerInDb);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
