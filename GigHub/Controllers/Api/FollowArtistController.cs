using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [Authorize]
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


        public IHttpActionResult Follow(FollowArtistDto dto)
        {
            var currentUser = User.Identity.GetUserId();

            if (dto.ArtistId == currentUser)
                return BadRequest("Cant follow yourself");

            if (_context.Following.Any(f => f.FolloweeId == currentUser && f.FolloweeId == dto.ArtistId))
                return BadRequest("Following already exists.");

            var followArtist = new Following
            {
                FolloweeId = dto.ArtistId,
                FollowerId = currentUser
            };

            _context.Following.Add(followArtist);
            _context.SaveChanges();

            return Ok();
        }
    }
}