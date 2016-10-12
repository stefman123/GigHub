using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Respositories
{
    public class GigRepository : IGigRepository
    {
        private IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetFutureGigsByUserId(string logInUserId)
        {
           return _context.Gigs
               .Where(g => 
                           g.ArtistId == logInUserId 
                           && g.DateTime > DateTime.Now 
                           && !g.IsCanceled)
               .Include(g => g.Genre)
               .ToList();
        }

        public IEnumerable<Gig> GetFutureGigs()
        {
               return _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
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
                .Include(g => g.Artist)
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
                    .Where( a => a.DateTime >= DateTime.Now)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}