using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.Persistence;
using GigHub.Respositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
 
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a New Gig"
            };

            return View("GigForm",viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
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
            var upcomingGigs = _unitOfWork.Gigs.GetFutureGigs(loggedUserId);

            return View(upcomingGigs);
        }

        public ActionResult GigDetails(int id)
        {
            var currentUser = User.Identity.GetUserId();

            //var gig = _context.Gigs.Single(g => g.Id == id);

            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GigViewModel { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsAttending = _unitOfWork.Attendances.CheckAttendences(currentUser, gig);

                viewModel.IsFollowing = _unitOfWork.Followers.CheckFollowing(currentUser, gig);

            }

            return View("GigDetails", viewModel);
        }

        [Authorize]
        public ActionResult GigsAttending()
        {
            var logInUserId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(logInUserId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs Im Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(logInUserId).ToLookup(a => a.GigId)

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
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
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

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("CurrentGigs","Gigs");

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if(gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();


            gig.Venue = viewModel.Venue;
            gig.DateTime = viewModel.GetDateTime();
            gig.GenreId = viewModel.Genre;

            gig.Amend(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("CurrentGigs", "Gigs");

        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchChars});
        }
    }
}