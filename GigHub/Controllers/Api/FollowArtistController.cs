using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Services.Description;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class FollowArtistController : ApiController
    {
        ApplicationDbContext _context;

        public FollowArtistController()
        {
            _context = new ApplicationDbContext();
        }


        //        [HttpGet]
        //        public IHttpActionResult CheckFollowing(string ArtistDetailId)
        //        {

        //            var currentUser = User.Identity.GetUserId();

        //            //var yummy = ArtistDetail;
        //            var yum = ArtistDetailId;

        //           var hmmm = _context.Following.Single(f => f.FolloweeId == ArtistDetailId);
        //            //var followArtist = new Following();
        //            //if (_context.Following.Any(f => f.FolloweeId == currentUser && f.FolloweeId == dto.ArtistId))
        //            //_context.Following.Add(followArtist);
        //            //_context.SaveChanges();
        //            return Ok();
        ///*
        //            return BadRequest("Cant follow yourself");
        //*/

        //        }

            [System.Web.Http.HttpPost]
        public IHttpActionResult Follow(FollowArtistDetailDto dto)
        {
            var currentUser = User.Identity.GetUserId();

            if (dto.ArtistDetailId == currentUser)
                return BadRequest("Cant follow yourself");

            if (_context.Following.Any(f => f.FolloweeId == dto.ArtistDetailId && f.FollowerId ==currentUser ))
                return BadRequest("Following already exists.");
            var followArtist = new Following
            {
                FolloweeId = dto.ArtistDetailId,
                FollowerId = currentUser
            };

            _context.Following.Add(followArtist);
            _context.SaveChanges();

            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteFollowing(string id)
        {
            var currentUser = User.Identity.GetUserId();

            if (id == currentUser)
                return BadRequest("Cant follow yourself");

            var following =
                _context.Following.SingleOrDefault(
                    f => f.FollowerId == currentUser && f.FolloweeId == id);

            if (following == null)
                return NotFound();

                _context.Following.Remove(following);
                _context.SaveChanges();


            return Ok();
        }
    }
}