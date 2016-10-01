using GigHub.Respositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followers { get; set; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }

        void Complete();
    }
}