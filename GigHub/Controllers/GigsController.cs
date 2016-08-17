﻿using System;
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
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }
        [Authorize]
        public ActionResult CurrentGigs()
        {
            var loggedUserId = User.Identity.GetUserId();
            var upcomingGigs = _context.Gigs
                .Where(g => g.ArtistId == loggedUserId && g.DateTime > DateTime.Now)
                .Include(g=>g.Genre)
                .ToList();

            return View(upcomingGigs);
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

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigsAttending,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs Im Attending"
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
                return View("Create", viewModel);
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
    }
}