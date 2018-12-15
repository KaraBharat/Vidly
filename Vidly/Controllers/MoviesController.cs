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

        public ActionResult New()
        {
            var newMovie = new MovieFormViewModels()
            {
                Genre = _dbContext.Genre.ToList()
            };
            return View("MovieForm", newMovie);
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}