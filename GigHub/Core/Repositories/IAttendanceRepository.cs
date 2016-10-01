using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        bool CheckAttendences(string currentUser, Gig gig);
        IEnumerable<Attendence> GetFutureAttendances(string currentUser);
    }
}