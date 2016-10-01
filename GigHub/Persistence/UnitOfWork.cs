using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence.Respositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }

        public IFollowingRepository Followers  { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Genres = new GenreRepository(context);
            Attendances = new AttendanceRepository(context);
            Followers = new FollowingRepository(context);

        }


        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}