using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.Respositories
{

    public class FollowingRepository
    {
        private ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckFollowing(string currentUser, Gig gig)
        {
           return _context.Following
                    .Any(f => f.FolloweeId == gig.ArtistId && f.FollowerId == currentUser);
        }

    }
}