using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Respositories
{

    public class FollowingRepository : IFollowingRepository
    {
        private ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckFollowing(string logInUserId, Gig gig)
        {
           return _context.Following
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == logInUserId);
        }

        public IEnumerable<ApplicationUser> GetFollowings( string logInUserId)
        {

           return _context.Following.Where(a => a.FollowerId == logInUserId)
                .Select(a => a.Followee)
                .ToList();
        }

    }
}