using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Respositories
{
    public class UserNotificationsRespository : IUserNotificationsRespository
    {
        private readonly IApplicationDbContext _context;

        public UserNotificationsRespository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUserNotificationsForUser (string userId)
        {
             return _context.UserNotifications
                    .Where(un => un.UserId == userId && !un.IsRead)
                    .ToList();
        }

 
    }
}