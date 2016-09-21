using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    public class TestingController : ApiController
    {
        ApplicationDbContext _context;

        public TestingController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult CheckFollowing(TestDto dto)
        {
            var currentUser = User.Identity.GetUserId();

             var isFollowing = _context.Following.Any(f => f.FolloweeId == dto.ArtistTestId && f.FollowerId == currentUser);

            if (isFollowing == true)
          return Ok();

            return NotFound();

        }
    }
}
