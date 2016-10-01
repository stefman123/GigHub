using System;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Respositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
             _context = context;
        }

        public IEnumerable<Attendence> GetFutureAttendances(string currentUser)
        {
            return _context.Attendences
                .Where(a => a.AttendeeId == currentUser && a.Gig.DateTime >= DateTime.Now)
                .ToList();
        }

        public bool CheckAttendences(string currentUser, Gig gig)
        {
           return _context.Attendences
                    .Any(a => a.GigId == gig.Id && a.AttendeeId == currentUser);
        }
    }
}