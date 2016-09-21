using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
 
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Heading = "Add a New Gig"
            };

            return View("GigForm",viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a Gig",
                Id = gig.Id
            };


            return View("GigForm",viewModel);
        }

        [Authorize]
        public ActionResult CurrentGigs()
        {
            var loggedUserId = User.Identity.GetUserId();
            var upcomingGigs = _context.Gigs
                .Where(g => g.ArtistId == loggedUserId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g=>g.Genre)
                .ToList();

            return View(upcomingGigs);
        }

        public ActionResult GigDetails(int id)
        {
            var loggedUserId = User.Identity.GetUserId();

            //var gig = _context.Gigs.Single(g => g.Id == id);

            var gig = _context.Gigs
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Single(g => g.Id == id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GigViewModel
            {
                Gig = gig             
            };

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendences
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == currentUser);

                viewModel.IsFollowing = _context.Following
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == currentUser);

            }

            return View("GigDetails", viewModel);
        }

        [Authorize]
        public ActionResult GigsAttending()
        {
            var logInUserId = User.Identity.GetUserId();
            var gigsAttending = _context.Attendences
                .Where(a => a.AttendeeId == logInUserId)
                .Select(a => a.Gig)
                .Include( g => g.Artist)
                .Include(g => g.Genre)
                .ToList();


            var attendances = _context.Attendences.Where(
                a => a.AttendeeId == logInUserId && a.Gig.DateTime >= DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigsAttending,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs Im Attending",
                Attendances =  attendances

            };

            return View("Gigs",viewModel);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            //returns the application user to add a gig to
            //var artistId = User.Identity.GetUserId();

            //var artist = _context.Users.Single(u => u.Id == artistId);
            //var genre = _context.Genres.Single(g => g.Id == viewModel.Genre);

            var   gig = new Gig
            {
                //var artistId = User.Identity.GetUserId();
                ArtistId = User.Identity.GetUserId(),
                Venue = viewModel.Venue,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre

            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("CurrentGigs","Gigs");

        }        

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == viewModel.Id && g.ArtistId == userId);

            gig.Venue = viewModel.Venue;
            gig.DateTime = viewModel.GetDateTime();
            gig.GenreId = viewModel.Genre;

            gig.Amend(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);

            _context.SaveChanges();

            return RedirectToAction("CurrentGigs","Gigs");

        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchChars});
        }
    }
}