using System.Collections.Generic;
using GigHub.Models;

namespace GigHub.Respositories
{
    public interface IAttendanceRepository
    {
        bool CheckAttendences(string currentUser, Gig gig);
        IEnumerable<Attendence> GetFutureAttendances(string currentUser);
    }
}