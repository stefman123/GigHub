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

        public bool CheckAttendences(string currentUser, int gigId)
        {
           return _context.Attendences
                    .Any(a => a.GigId == gigId && a.AttendeeId == currentUser);
        }

        public Attendence GetAttendence(string currentUser, int gigId)
        {
           return _context.Attendences
                    .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == currentUser);
        }

        public void Add(Attendence attendence)
        {
            _context.Attendences.Add(attendence);
        }

        public void Remove(Attendence attendence)
        {
            _context.Attendences.Remove(attendence);
        }
    }
}