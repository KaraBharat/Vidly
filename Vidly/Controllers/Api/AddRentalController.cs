using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class AddRentalController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public AddRentalController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET api/AddRental
        public IHttpActionResult GetRental()
        {

            return Ok();
        }

        [HttpPost]
        // POST api/AddRental
        public IHttpActionResult AddRental(RentalDto rentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please check input parameters");

            if (rentalDto.Movies.Count == 0)
                return BadRequest("Please provide movies");

            var customer = _dbContext.Customer.SingleOrDefault(c => c.Id == rentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Invalid customer");

            var movies = _dbContext.Movie.Where(m => rentalDto.Movies.Contains(m.Id)).ToList();

            if (movies == null)
                return BadRequest("Invalid movies");

            if (movies.Count != rentalDto.Movies.Count)
                return BadRequest("One or more movies not found");

            foreach (var movieId in rentalDto.Movies)
            {
                var currentMovie = movies.Single(m => m.Id == movieId);

                if (currentMovie.NumberAvailable == 0)
                {
                    return BadRequest("One or more movies not available");
                }
                else
                {
                    Rental rentalData = new Rental
                    {
                        CustomerId = rentalDto.CustomerId,
                        MovieId = movieId,
                        RentalDate = DateTime.Now,
                    };

                    _dbContext.Rental.Add(rentalData);

                    currentMovie.NumberAvailable--;
                }
            }

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
