using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        IEnumerable<Gig> GetFutureGigsByUserId(string logInUserId);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string logInUserId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetFutureGigs();
    }
}