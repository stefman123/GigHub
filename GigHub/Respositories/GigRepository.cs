using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.Respositories
{
    public class GigRepository
    {
        private ApplicationDbContext _context;

        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetFutureGigs(string logInUserId)
        {
           return _context.Gigs
               .Where(g => g.ArtistId == logInUserId && g.DateTime > DateTime.Now && !g.IsCanceled)
               .Include(g => g.Genre)
               .ToList();
        }

        public Gig GetGig(int gigId)
        {
          return _context.Gigs
                            .Include(a => a.Artist)
                            .Include(a => a.Genre)
                            .SingleOrDefault(g => g.Id == gigId);
        }
        
        public Gig GetGigWithAttendees(int gigId)
        {
           return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);

        }

        //public Gig GetGigDetails(int gigId )
        //{
        //    _context.Gigs

        //        .Single(g => g.Id == gigId);

        //}

        public IEnumerable<Gig> GetGigsUserAttending(string logInUserId)
        {
            return _context.Attendences
                .Where(a => a.AttendeeId == logInUserId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }
    }
}