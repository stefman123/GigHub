using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using GigHub.Respositories;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index( string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) ||
                                                       g.Genre.Name.Contains(query) ||
                                                       g.Venue.Contains(query));
            }

            string currentUserId = User.Identity.GetUserId();

            var attendances = _attendanceRepository.GetFutureAttendances(currentUserId).ToLookup(a => a.GigId);
                                                            
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