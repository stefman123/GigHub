using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followers { get; set; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        INotificationsRepository Notifications { get; }
        IUserNotificationsRespository UserNotifications { get; }

        

        void Complete();
    }
}