using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        bool CheckAttendences(string currentUser, int gigId);
        Attendence GetAttendence(string currentUser, int gigId);
        IEnumerable<Attendence> GetFutureAttendances(string currentUser);
        void Add(Attendence attendence);
        void Remove(Attendence attendence);
    }
}