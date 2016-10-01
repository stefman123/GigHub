using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Respositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private IUnitOfWork _unitOfWork;
        public HomeController( IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;

        }

        public ActionResult Index( string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetFutureGigs();


            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                                       g.Genre.Name.Contains(query) ||
                                                       g.Venue.Contains(query));
            }

            string currentUserId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(currentUserId).ToLookup(a => a.GigId);
                                                            
            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchChars = query,
                Attendances = attendances

            };

            return View("Gigs",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}