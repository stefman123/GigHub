using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Respositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);
        IEnumerable<Gig> GetFutureGigs(string logInUserId);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string logInUserId);
        Gig GetGigWithAttendees(int gigId);
    }
}