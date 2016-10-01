using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        bool CheckFollowing(string logInUserId, Gig gig);
        IEnumerable<ApplicationUser> GetFollowings (string logInUserId);
    }
}