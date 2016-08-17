using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Artist
        public ActionResult Index()
        {
            var logInUserId = User.Identity.GetUserId();

            var artistFollowing =
                _context.Following.Where(a => a.FollowerId == logInUserId)
                .Select(a => a.Followee)
                .ToList();
            
            return View(artistFollowing);
        }
    }
}