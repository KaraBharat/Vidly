using AutoMapper;
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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET api/movies
        public IHttpActionResult GetMovies()
        {
            var movies = _dbContext.Movie.ToList().Select(Mapper.Map<Movie, MovieDto>);

            if (movies == null || !movies.Any())
                return NotFound();

            return Ok(movies);
        }

        // GET api/movies/1
        public IHttpActionResult GetMoviesById(int Id)
        {
            var movie = _dbContext.Movie.SingleOrDefault(s => s.Id == Id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovies(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _dbContext.Movie.Add(movie);
            _dbContext.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        [HttpPut]
        // PUT api/movies/1
        public IHttpActionResult UpdateMovies(int Id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _dbContext.Movie.SingleOrDefault(s => s.Id == Id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieInDb, movieInDb);

            _dbContext.SaveChanges();

            return Ok(movieDto);
        }

        [HttpDelete]
        // DELETE api/movies/1
        public IHttpActionResult DeleteMovies(int Id)
        {
            var movieInDb = _dbContext.Movie.SingleOrDefault(s => s.Id == Id);

            if (movieInDb == null)
                return NotFound();

            _dbContext.Movie.Remove(movieInDb);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
