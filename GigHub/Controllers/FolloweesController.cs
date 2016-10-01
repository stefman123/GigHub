using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Respositories;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Artist
        public ActionResult Index()
        {
            var logInUserId = User.Identity.GetUserId();

            var artistFollowing = _unitOfWork.Followers.GetFollowings(logInUserId);

            
            return View(artistFollowing);
        }
    }
}