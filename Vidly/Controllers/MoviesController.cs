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
    public class MoviesController : Controller
    {
        private ApplicationDbContext _dbContext;

        public MoviesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Movies
        public ActionResult Index()
        {
            var movieViewModel = new MovieViewModels();

            movieViewModel.Movies = _dbContext.Movie.Include(m => m.Genre).ToList();

            return View(movieViewModel);
        }

        [Route("movies/details/{Id}")]
        public ActionResult Detail(int Id)
        {
            var movie = _dbContext.Movie.Where(s => s.Id == Id).Include(c => c.Genre).FirstOrDefault();

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Route("movies/edit/{Id}")]
        public ActionResult Edit(int Id)
        {
            var movie = _dbContext.Movie.Where(s => s.Id == Id).Include(c => c.Genre).FirstOrDefault();

            var editMovie = new MovieFormViewModels()
            {
                Movie = movie,
                Genre = _dbContext.Genre.ToList()
            };
            return View("MovieForm", editMovie);
        }

        public ActionResult New()
        {
            var newMovie = new MovieFormViewModels()
            {
                Genre = _dbContext.Genre.ToList()
            };
            return View("MovieForm", newMovie);
        }

        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.AddedOn = DateTime.Now;
                _dbContext.Movie.Add(movie);
            }
            else
            {
                var movieInDb = _dbContext.Movie.Where(s => s.Id == movie.Id).Include(c => c.Genre).First();
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}