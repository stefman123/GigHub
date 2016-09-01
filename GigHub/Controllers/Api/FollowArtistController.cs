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


        public IHttpActionResult Follow( FollowArtistDto dto)
        {
            var currentUser = User.Identity.GetUserId();

            if (dto.ArtistId == currentUser)
                return BadRequest("Cant follow yourself");

            if (_context.Following.Any(f=> f.FolloweeId == currentUser &&  f.FolloweeId == dto.ArtistId))
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