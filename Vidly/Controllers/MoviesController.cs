using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            var movieViewModel = new MovieViewModels();
            var movies = new List<Movie>();

            movies.Add(new Movie { Id = 1, Name = "Dead Pool" });
            movies.Add(new Movie { Id = 2, Name = "Die Hard" });

            movieViewModel.Movies = movies;

            return View(movieViewModel);
        }
    }
}