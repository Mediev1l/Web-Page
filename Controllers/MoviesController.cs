using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieFormViewModel movieFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                var genre = _context.Genres.ToList();

                var view = new MovieFormViewModel
                {
                    Name = movieFormViewModel.Name,
                    ReleaseDate = movieFormViewModel.ReleaseDate,
                    GenreId = movieFormViewModel.GenreId,
                    StockNumber = movieFormViewModel.StockNumber,
                    Genres = genre
                    
                };

                return View("MoviesForm", view);
            }
       
            if (movieFormViewModel.Id == 0)
            {
                var mov = new Movie
                {
                    Name = movieFormViewModel.Name,
                    ReleaseDate = movieFormViewModel.ReleaseDate.GetValueOrDefault(),
                    GenreId = movieFormViewModel.GenreId.GetValueOrDefault(),
                    StockNumber = movieFormViewModel.StockNumber.GetValueOrDefault()

                };

                _context.Movies.Add(mov);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movieFormViewModel.Id);
                movieInDb.Name = movieFormViewModel.Name;
                movieInDb.ReleaseDate = movieFormViewModel.ReleaseDate.GetValueOrDefault();
                movieInDb.GenreId = movieFormViewModel.GenreId.GetValueOrDefault();
                movieInDb.StockNumber = movieFormViewModel.StockNumber.GetValueOrDefault();
            }

            try
            {
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            

            return View("MoviesForm", viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View("MoviesForm", viewModel);
        }
            

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre);

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
            return View(movies);
        }



    }
}