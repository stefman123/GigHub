using GigHub.Models;

namespace GigHub.Respositories
{
    public interface IFollowingRepository
    {
        bool CheckFollowing(string currentUser, Gig gig);
    }
}