using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Respositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IApplicationDbContext _context;
        public NotificationsRepository(IApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Notification> GetNotificationsForUser(string userId)
        {
            return _context.UserNotifications
                .Where(un =>
                            un.UserId == userId 
                           && !un.IsRead
                       )
                .Select(n => n.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();
        }



        
    }
}