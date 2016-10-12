using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationsRespository
    {
        IEnumerable<UserNotification> GetUserNotificationsForUser (string userId);
    }
}